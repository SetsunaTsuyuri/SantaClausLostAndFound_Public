using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using KanKikuchi.AudioManager;

namespace DungeonRPG3D
{
    /// <summary>
    /// フィールド
    /// </summary>
    public class Field : MonoBehaviour
    {
        /// <summary>
        /// フィールドデータ
        /// </summary>
        [field: SerializeField]
        public FieldData Data { get; private set; } = null;

        /// <summary>
        /// オブジェクト生成装置リスト
        /// </summary>
        [field: SerializeField]
        public List<ObjectOnFieldGenerator> ObjectOnFieldGenerators { get; private set; } = new List<ObjectOnFieldGenerator>();

        /// <summary>
        /// BGM
        /// </summary>
        [field: SerializeField]
        public BGMAssistant BGM { get; private set; } = null;

        /// <summary>
        /// トランスフォーム
        /// </summary>
        [field: SerializeField]
        public Transform SelfTransform { get; private set; } = null;

        /// <summary>
        /// タイルマップの親オブジェクト
        /// </summary>
        [field: SerializeField]
        public Transform TiledMapBlocksParent { get; private set; } = null;

        private void Reset()
        {
            ChacheComponent();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        private void ChacheComponent()
        {
            SelfTransform = transform;
            TiledMapBlocksParent = SelfTransform.Find("TiledMap");
            ObjectOnFieldGenerators = GetComponentsInChildren<ObjectOnFieldGenerator>().ToList();
            BGM = GetComponent<BGMAssistant>();
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        public void Init()
        {
            CreateTiledMapBlocks();

            foreach (var genarator in ObjectOnFieldGenerators)
            {
                genarator.Generate();
            }
        }

        /// <summary>
        /// データからブロックを生成する
        /// </summary>
        private void CreateTiledMapBlocks()
        {
            if (!Data)
            {
                return;
            }

            for (int y = 0; y < Data.Height; y++)
            {
                for (int x = 0; x < Data.Width; x++)
                {
                    CreateUnderObject(x, y);
                    CreateMiddleObject(x, y);
                    CreateUpperObject(x, y);
                }
            }
        }

        /// <summary>
        /// 下層オブジェクトを生成する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        private void CreateUnderObject(int x, int y)
        {
            GameObject prefab = GetUnderPrefab(x, y);
            if (!prefab)
            {
                return;
            }

            Vector3 position = new Vector3(x, 0.0f, y);
            Quaternion quaternion = Quaternion.identity;

            Instantiate(prefab, position, quaternion, TiledMapBlocksParent);
        }

        /// <summary>
        /// 中層オブジェクトを生成する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        private void CreateMiddleObject(int x, int y)
        {
            GameObject prefab = GetMiddlePrefab(x, y);
            if (!prefab)
            {
                return;
            }

            Vector3 position = new Vector3(x, 0.0f, y);
            Quaternion quaternion = GetMiddleObjectQuaternion(x, y);

            Instantiate(prefab, position, quaternion, TiledMapBlocksParent);
        }

        /// <summary>
        /// 上層オブジェクトを生成する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        private void CreateUpperObject(int x, int y)
        {
            GameObject prefab = GetUpperPrefab(x, y);
            if (!prefab)
            {
                return;
            }

            Vector3 position = new Vector3(x, 0.0f, y);
            Quaternion quaternion = Quaternion.identity;

            Instantiate(prefab, position, quaternion, TiledMapBlocksParent);
        }


        /// <summary>
        /// 下層プレファブを取得する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>下層プレファブ</returns>
        private GameObject GetUnderPrefab(int x, int y)
        {
            GameObject prefab = null;

            switch (Data.GetCell(x, y).MyUnderType)
            {
                case Cell.UnderType.Floor:
                    // A、Bを交互に配置する
                    int modFloor = y % 2 == 0 ? 0 : 1;
                    if (Data.ToIndex(x, y) % 2 == modFloor)
                    {
                        prefab = Data.FloorBlockA;
                    }
                    else
                    {
                        prefab = Data.FloorBlockB;
                    }

                    break;

                case Cell.UnderType.Water:
                    prefab = Data.WaterBlock;
                    break;

                case Cell.UnderType.Wall:
                    // A、Bを交互に配置する
                    int modWall = y % 2 == 0 ? 0 : 1;
                    if (Data.ToIndex(x, y) % 2 == modWall)
                    {
                        prefab = Data.WallBlockA;
                    }
                    else
                    {
                        prefab = Data.WallBlockB;
                    }

                    break;
            }

            return prefab;
        }

        /// <summary>
        /// 中層プレファブを取得する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>中層プレファブ</returns>
        private GameObject GetMiddlePrefab(int x, int y)
        {
            GameObject prefab = null;

            switch (Data.GetCell(x, y).MyMiddleType)
            {
                case Cell.MiddleType.Bridge:
                    prefab = Data.Bridge;
                    break;
            }

            return prefab;
        }

        /// <summary>
        /// 上層プレファブを取得する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>上層プレファブ</returns>
        private GameObject GetUpperPrefab(int x, int y)
        {
            GameObject prefab = null;

            switch (Data.GetCell(x, y).MyUpperType)
            {
                case Cell.UpperType.Grass:
                    // A、Bを交互に配置する
                    int mod = y % 2 == 0 ? 0 : 1;
                    if (Data.ToIndex(x, y) % 2 == mod)
                    {
                        prefab = Data.GrassA;
                    }
                    else
                    {
                        prefab = Data.GrassB;
                    }
                    break;

                case Cell.UpperType.Tree:
                    prefab = Data.Tree;
                    break;
            }

            return prefab;
        }

        /// <summary>
        /// 中層オブジェクトのクォータニオンを取得する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>橋に設定するべきクォータニオン</returns>
        private Quaternion GetMiddleObjectQuaternion(int x, int y)
        {
            // 基本的には回転しない
            Quaternion quaternion = Quaternion.identity;

            // 左のセル
            Cell leftCell = Data.GetCell(x - 1, y);

            // 右のセル
            Cell rightCell = Data.GetCell(x + 1, y);

            // 領域外を指定した場合は中止する
            if (leftCell == null || rightCell == null)
            {
                return quaternion;
            }

            // 隣に床または橋がある場合
            if (leftCell.MyUnderType == Cell.UnderType.Floor ||
                rightCell.MyUnderType == Cell.UnderType.Floor ||
                leftCell.MyMiddleType == Cell.MiddleType.Bridge ||
                rightCell.MyMiddleType == Cell.MiddleType.Bridge)
            {
                // Y軸のみ90度回転
                quaternion = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }

            return quaternion;
        }

        /// <summary>
        /// プレイヤーの位置を初期化する
        /// </summary>
        public void InitPositonsOfPlayer()
        {
            ObjectsOnFieldManager manager = ManagersMaster.Instance.ObjectsOnFieldM;
            if (!manager.isActiveAndEnabled)
            {
                return;
            }

            Player player = manager.Player;
            Vector2Int position = Data.InitialPositonOfPlayer;
            Creature.Direction direction = Data.InitialDirectionOfPlayer;

            player.SelfMove.Teleport(position, direction);
        }

        /// <summary>
        /// 敵の撃破状況を再現する
        /// </summary>
        public void ReproduceEnemiesDefeatingState()
        {
            GameController gameController = GameController.Instance;
            if (!gameController.ExistEnemiesDefeatingStatusInCurrentField())
            {
                return;
            }

            ObjectsOnFieldManager objectsOnFieldManager = ManagersMaster.Instance.ObjectsOnFieldM;
            if (!objectsOnFieldManager.isActiveAndEnabled)
            {
                return;
            }

            Enemy[] enemies = objectsOnFieldManager.Enemies;
            bool[] defeating = GameController.Instance.GetGiftBoxesCollectionStatusInCurrentField();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (defeating[i])
                {
                    enemies[i].OnBeingDefeatedAlready();
                }
            }
        }


        /// <summary>
        /// プレゼント箱の回収状況を再現する
        /// </summary>
        public void ReproduceGiftBoxeCollectionState()
        {
            GameController gameController = GameController.Instance;
            if (!gameController.ExistGiftBoxesCollectionStatusInCurrentField())
            {
                return;
            }

            ObjectsOnFieldManager objectsOnFieldManager = ManagersMaster.Instance.ObjectsOnFieldM;
            if (!objectsOnFieldManager.isActiveAndEnabled)
            {
                return;
            }

            GiftBox[] boxes = objectsOnFieldManager.GiftBoxes;
            bool[] collectionStatus = GameController.Instance.GetGiftBoxesCollectionStatusInCurrentField();
            for (int i = 0; i < boxes.Length; i++)
            {
                if (collectionStatus[i])
                {
                    boxes[i].OnBeingObtainedAlreay();   
                }
            }
        }
    }
}
