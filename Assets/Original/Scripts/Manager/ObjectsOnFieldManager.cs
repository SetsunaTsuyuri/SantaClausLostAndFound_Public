using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// フィールド上のオブジェクト管理者
    /// </summary>
    public class ObjectsOnFieldManager : ManagerBase
    {
        /// <summary>
        /// プレイヤープレファブの配列
        /// </summary>
        [field: SerializeField]
        public Player[] PlayerPrefabs { get; private set; } = { };

        /// <summary>
        /// 生成されるプレイヤーとして選ばれたプレイヤープレファブのインデックス
        /// </summary>
        public int SelectedPlayerPrefabArrayIndex { get; private set; } = 0;

        /// <summary>
        /// フィールドの全オブジェクトのリスト
        /// </summary>
        public List<ObjectOnField> ObjectsOnField { get; private set; } = new List<ObjectOnField>();

        /// <summary>
        /// プレイヤー
        /// </summary>
        public Player Player { get; private set; } = null;

        /// <summary>
        /// 敵
        /// </summary>
        public Enemy[] Enemies { get; private set; } = { };

        /// <summary>
        /// プレゼント箱
        /// </summary>
        public GiftBox[] GiftBoxes { get; private set; } = { };

        /// <summary>
        /// ゴールオブジェクト
        /// </summary>
        public Goal Goal { get; private set; } = null;

        /// <summary>
        /// プレイヤーデータ
        /// </summary>
        [field: SerializeField]
        public PlayerDataList PlayerDataList { get; private set; } = null;

        /// <summary>
        /// 敵データ
        /// </summary>
        [field: SerializeField]
        public EnemyDataList EnemyDataList { get; private set; } = null;

        /// <summary>
        /// プレゼント箱データ
        /// </summary>
        [field: SerializeField]
        public GiftBoxDataList GiftBoxDataList { get; private set; } = null;

        /// <summary>
        /// プレイヤー共通の設定
        /// </summary>
        [field: SerializeField]
        public PlayerSetting PlayerSetting { get; private set; } = null;

        /// <summary>
        /// 初期化する
        /// </summary>
        /// <param name="field">フィールド</param>
        public void Init(Field field)
        {
            Player playerInActiveScene = GetPlayerInActiveScene();
            if (playerInActiveScene == null)
            {
                Player playerPrefab = GetSelectedPlayerPrefab();
                Player = Instantiate(playerPrefab);
                DontDestroyOnLoad(Player);
            }
            else
            {
                Player = playerInActiveScene;
            }

            Enemies = field.GetComponentsInChildren<Enemy>();
            GiftBoxes = field.GetComponentsInChildren<GiftBox>();
            Goal = field.GetComponentInChildren<Goal>();

            // リストに加える
            AddAllObjectsOnField();

            // 全フィールドオブジェクトを初期化する
            InitAllObjectsOnField();
        }

        /// <summary>
        /// シーン内のプレイヤーを探す
        /// </summary>
        /// <returns>プレイヤー(いなければnull)</returns>
        public Player GetPlayerInActiveScene()
        {
            Player player = null;

            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.GetComponent<Player>();
            }

            return player;
        }

        /// <summary>
        /// 選択されたプレイヤープレファブを取得する
        /// </summary>
        /// <returns>プレイヤープレファブ</returns>
        public Player GetSelectedPlayerPrefab()
        {
            return PlayerPrefabs[SelectedPlayerPrefabArrayIndex];
        }

        /// <summary>
        /// 生成するプレイヤープレファブのインデックスを選ぶ
        /// </summary>
        /// <param name="newIndex">配列何番目か</param>
        public void SelectPlayerPrefabArrayIndex(int newIndex)
        {
            SelectedPlayerPrefabArrayIndex = newIndex;
        }

        /// <summary>
        /// 全てのフィールドオブジェクトをリストに加える
        /// </summary>
        private void AddAllObjectsOnField()
        {
            // プレイヤー
            if (Player)
            {
                ObjectsOnField.Add(Player);
            }

            // 敵
            if (Enemies != null)
            {
                foreach (var enemy in Enemies)
                {
                    ObjectsOnField.Add(enemy);
                }
            }

            // プレゼント箱
            if (GiftBoxes != null)
            {
                foreach (var box in GiftBoxes)
                {
                    ObjectsOnField.Add(box);
                }
            }

            // ゴール
            if (Goal)
            {
                ObjectsOnField.Add(Goal);
            }
        }

        /// <summary>
        /// 全てのフィールドオブジェクトを初期化する
        /// </summary>
        private void InitAllObjectsOnField()
        {
            foreach (var item in ObjectsOnField)
            {
                item.Init();
            }
        }

        /// <summary>
        /// 戦闘態勢ではない敵を非アクティブにする
        /// </summary>
        public void DeactivateEnemiesThatAreNotInBattle()
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].CurrentState != Creature.State.InBattle)
                {
                    Enemies[i].gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// 生きている敵をアクティブにする
        /// </summary>
        public void ActivateLivingEnemies()
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].CurrentState != Creature.State.Dead)
                {
                    Enemies[i].gameObject.SetActive(true);
                }
            }
        }

        /// <summary>
        /// フィールド上の死亡している敵の数を取得する
        /// </summary>
        /// <returns>死亡している敵の数</returns>
        public int CountDeadEnemies()
        {
            int deadEnemies = 0;

            foreach (var enemy in Enemies)
            {
                if (enemy.CurrentState == Creature.State.Dead)
                {
                    deadEnemies++;
                }
            }

            return deadEnemies;
        }

        /// <summary>
        /// フィールド上の敵の数を取得する
        /// </summary>
        /// <returns>敵の数</returns>
        public int CountEnemies()
        {
            return Enemies.Length;
        }

        /// <summary>
        /// フィールド上の回収済みのプレゼント箱の数を取得する
        /// </summary>
        /// <returns>回収済みプレゼントの数</returns>
        public int CountCollectedGiftBoxes()
        {
            int opendBoxes = 0;

            foreach (var box in GiftBoxes)
            {
                if (box.CurrentState == GiftBox.State.Collected)
                {
                    opendBoxes++;
                }
            }

            return opendBoxes;
        }

        /// <summary>
        /// フィールド上のプレゼント箱の数を取得する
        /// </summary>
        /// <returns>プレゼント箱の数</returns>
        public int CountGiftBoxes()
        {
            return GiftBoxes.Length;
        }
    }
}
