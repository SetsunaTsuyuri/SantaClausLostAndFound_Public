using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

namespace DungeonRPG3D
{
    /// <summary>
    /// ゲーム全体の操作
    /// </summary>
    public class GameController : SingletonMonoBehaviour<GameController>
    {
        /// <summary>
        /// BGMを継続して流すか
        /// </summary>
        public bool ContinueBGM { get; set; } = false;

        /// <summary>
        /// プレイヤーが倒されたか
        /// </summary>
        public bool PlayerHasDefeated { get; set; } = false;

        /// <summary>
        /// 選択されたステージデータのインデックス
        /// </summary>
        public int SelectedStageDataIndex { get; private set; } = 0;

        /// <summary>
        /// 現在のフィールドレベル
        /// </summary>
        public int CurrentFieldLevel { get; private set; } = 0;

        /// <summary>
        /// 死亡した敵の総数
        /// </summary>
        public int TotalDeadEnemies { get; private set; } = -1;

        /// <summary>
        /// 敵の総数
        /// </summary>
        public int TotalEnemies { get; private set; } = -1;

        /// <summary>
        /// 敵の死亡率
        /// </summary>
        public float DeadEnemiesRate { get; private set; } = -1.0f;

        /// <summary>
        /// 回収されたプレゼント箱の総数
        /// </summary>
        public int TotalCollectedGiftBoxes { get; private set; } = -1;

        /// <summary>
        /// プレゼント箱の総数
        /// </summary>
        public int TotalGiftBoxes { get; private set; } = -1;

        /// <summary>
        /// プレゼント箱の回収率
        /// </summary>
        public float CollectedGiftBoxesRate { get; private set; } = -1.0f;

        /// <summary>
        /// 各フィールドの敵の撃破状況
        /// </summary>
        public List<bool[]> EnemiesDefeatingStatusList { get; private set; } = new List<bool[]>();

        /// <summary>
        /// 各フィールドのプレゼント箱の回収状況
        /// </summary>
        public List<bool[]> GiftBoxesCollectionStatusList { get; private set; } = new List<bool[]>();

        protected override void AwakeInner()
        {
            base.AwakeInner();

            Init();
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        private void Init()
        {
            if (Instance != this)
            {
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 攻略するステージデータを選択する
        /// </summary>
        /// <param name="newIndex"></param>
        public void SelectStageData(int newIndex)
        {

            SelectedStageDataIndex = newIndex;

        }

        /// <summary>
        /// セレクトシーンで選択されたステージデータを取得する
        /// </summary>
        /// <returns>ステージデータ</returns>
        public StageData GetSelectedStageData()
        {
            FieldManager manager = ManagersMaster.Instance.FieldM;
            StageData stageData = manager.StageDataArray[SelectedStageDataIndex];
            return stageData;
        }

        /// <summary>
        /// 最大フィールドレベルを取得する
        /// </summary>
        /// <returns></returns>
        public int GetMaxFieldLevel()
        {
            StageData data = GetSelectedStageData();
            int level = data.Fields.Length - 1;
            return level;
        }

        /// <summary>
        /// ステージレベルを1増やす
        /// </summary>
        public void AddFieldLevel()
        {
            CurrentFieldLevel++;
        }

        /// <summary>
        /// ステージレベルを1減らす
        /// </summary>
        public void SubtaructFieldLevel()
        {
            CurrentFieldLevel--;
        }

        /// <summary>
        /// 次のフィールドへ行く前の更新処理
        /// </summary>
        public void UpdateBeforeNextFieldLevel()
        {
            UpdateEnemiesDefeatingStatus();
            UpdateGiftBoxesCollectionStatus();
        }

        /// <summary>
        /// 現在のフィールド内での敵の撃破状況を取得する
        /// </summary>
        /// <returns>敵の撃破状況</returns>
        public bool[] GetEnemiesDefeatingStatusInCurrentField()
        {
            return EnemiesDefeatingStatusList[CurrentFieldLevel];
        }

        /// <summary>
        /// 敵の撃破状況を更新する
        /// </summary>
        public void UpdateEnemiesDefeatingStatus()
        {
            ObjectsOnFieldManager manager = ManagersMaster.Instance.ObjectsOnFieldM;

            int enemyCount = manager.CountEnemies();
            bool[] defeatingStatus = new bool[enemyCount];

            Enemy[] enemies = manager.Enemies;
            for (int i = 0; i < enemyCount; i++)
            {
                if (enemies[i].IsDead())
                {
                    defeatingStatus[i] = true;
                }
            }

            if (!ExistEnemiesDefeatingStatusInCurrentField())
            {
                EnemiesDefeatingStatusList.Add(defeatingStatus);
            }
            else
            {
                EnemiesDefeatingStatusList[CurrentFieldLevel] = defeatingStatus;
            }
        }

        /// <summary>
        /// 現在のフィールドに対応した「敵の撃破状況の配列」が存在するか
        /// </summary>
        /// <returns></returns>
        public bool ExistEnemiesDefeatingStatusInCurrentField()
        {
            return CurrentFieldLevel < EnemiesDefeatingStatusList.Count;
        }

        /// <summary>
        /// 現在のフィールド内でのプレゼント箱の回収状況を取得する
        /// </summary>
        /// <returns>プレゼント箱の回収状況</returns>
        public bool[] GetGiftBoxesCollectionStatusInCurrentField()
        {
            return GiftBoxesCollectionStatusList[CurrentFieldLevel];
        }

        /// <summary>
        /// プレゼント箱の回収状況を更新する
        /// </summary>
        public void UpdateGiftBoxesCollectionStatus()
        {
            ObjectsOnFieldManager manager = ManagersMaster.Instance.ObjectsOnFieldM;

            int boxCount = manager.CountGiftBoxes();
            bool[] collectionStatus = new bool[boxCount];

            GiftBox[] boxes = manager.GiftBoxes;
            for (int i = 0; i < boxCount; i++)
            {
                if (boxes[i].IsCollected())
                {
                    collectionStatus[i] = true;
                }
            }

            if (!ExistGiftBoxesCollectionStatusInCurrentField())
            {
                GiftBoxesCollectionStatusList.Add(collectionStatus);
            }
            else
            {
                GiftBoxesCollectionStatusList[CurrentFieldLevel] = collectionStatus;
            }
        }

        /// <summary>
        /// 現在のフィールドに対応した「プレゼント箱回収状況の配列」が存在するか
        /// </summary>
        /// <returns></returns>
        public bool ExistGiftBoxesCollectionStatusInCurrentField()
        {
            return CurrentFieldLevel < GiftBoxesCollectionStatusList.Count;
        }

        /// <summary>
        /// ゲームデータを初期化する
        /// </summary>
        public void InitGameData()
        {
            PlayerHasDefeated = false;

            CurrentFieldLevel = 0;

            TotalEnemies = 0;
            TotalDeadEnemies = 0;
            DeadEnemiesRate = 0.0f;
            EnemiesDefeatingStatusList = new List<bool[]>();

            TotalGiftBoxes = 0;
            TotalCollectedGiftBoxes = 0;
            CollectedGiftBoxesRate = 0.0f;
            GiftBoxesCollectionStatusList = new List<bool[]>();
        }

        /// <summary>
        /// リザルトシーンのためのゲームデータを更新する
        /// </summary>
        public void UpdateGameDataForResultScene()
        {
            UpdateTotalEnemies();
            UpdateTotalBoxes();
            UpdateDeadEnemiesRate(TotalDeadEnemies, TotalEnemies);
            UpdateCollectedGiftBoxesRate(TotalCollectedGiftBoxes, TotalGiftBoxes);
        }

        /// <summary>
        /// 敵の総数・総撃破数を更新する
        /// </summary>
        public void UpdateTotalEnemies()
        {
            int enemies = 0;
            int deadEnemies = 0;

            foreach (var status in EnemiesDefeatingStatusList)
            {
                foreach (var val in status)
                {
                    if (val)
                    {
                        deadEnemies++;
                    }
                    enemies++;
                }
            }

            TotalEnemies = enemies;
            TotalDeadEnemies = deadEnemies;
        }

        /// <summary>
        /// 敵の死亡率を更新する
        /// </summary>
        /// <param name="dead">死亡している敵の総数</param>
        /// <param name="total">敵の総数</param>
        public void UpdateDeadEnemiesRate(int dead, int total)
        {
            DeadEnemiesRate = CalculationUtility.GetPercentage(dead, total);
        }

        /// <summary>
        /// プレゼント箱の総数・総回収数を更新する
        /// </summary>
        public void UpdateTotalBoxes()
        {
            int boxes = 0;
            int collectedBoxes = 0;

            foreach (var status in GiftBoxesCollectionStatusList)
            {
                foreach (var val in status)
                {
                    if (val)
                    {
                        collectedBoxes++;
                    }
                    boxes++;
                }
            }

            TotalGiftBoxes = boxes;
            TotalCollectedGiftBoxes = collectedBoxes;
        }

        /// <summary>
        /// プレゼント箱の回収率を更新する
        /// </summary>
        /// <param name="collected">回収済みの箱の総数</param>
        /// <param name="total">箱の総数</param>
        public void UpdateCollectedGiftBoxesRate(int collected, int total)
        {
            CollectedGiftBoxesRate = CalculationUtility.GetPercentage(collected, total);
        }

        /// <summary>
        /// ゲームを終了する
        /// </summary>
        public void QuitGame()
        {
            StartCoroutine(QuitGameCoroutine());
        }

        /// <summary>
        /// ゲーム終了コルーチン
        /// </summary>
        /// <returns></returns>
        public static IEnumerator QuitGameCoroutine()
        {
            yield return ManagersMaster.Instance.UIM.DoFadeOut();

#if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;

# elif UNITY_STANDALONE

            Application.Quit();
# endif
        }

    }
}
