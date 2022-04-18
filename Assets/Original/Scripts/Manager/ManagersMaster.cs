using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 各マネージャーの管理者
    /// </summary>
    public class ManagersMaster : SingletonMonoBehaviour<ManagersMaster>
    {
        /// <summary>
        /// 生物の管理者
        /// </summary>
        [field: SerializeField]
        public ObjectsOnFieldManager ObjectsOnFieldM { get; private set; } = null;

        /// <summary>
        /// フィールドの管理者
        /// </summary>
        [field: SerializeField]
        public FieldManager FieldM { get; private set; } = null;

        /// <summary>
        /// 冒険シーケンスの管理者
        /// </summary>
        [field: SerializeField]
        public AdventureManager AdventureM { get; private set; } = null;

        /// <summary>
        /// 戦闘シーケンスの管理者
        /// </summary>
        [field: SerializeField]
        public BattleManager BattleM { get; private set; } = null;

        /// <summary>
        /// UIアニメの管理者
        /// </summary>
        [field: SerializeField]
        public UIManager UIM { get; private set; } = null;

        /// <summary>
        /// カメラの管理者
        /// </summary>
        [field: SerializeField]
        public CameraManger CameraM { get; private set; } = null;

        /// <summary>
        /// エフェクトの管理者
        /// </summary>
        [field: SerializeField]
        public EffectManager EffectM { get; private set; } = null;

        /// <summary>
        /// 入力の管理者
        /// </summary>
        [field: SerializeField]
        public InputManager InputM { get; private set; } = null;

        /// <summary>
        /// アイテムの管理者
        /// </summary>
        [field: SerializeField]
        public ItemManager ItemM { get; private set; } = null;

        /// <summary>
        /// 行動データの管理者
        /// </summary>
        [field: SerializeField]
        public ActionDataManager ActionDataM { get; private set; } = null;

        /// <summary>
        /// シーン変化の管理者
        /// </summary>
        [field: SerializeField]
        public SceneChangeManager SceneChangeM { get; private set; } = null;

        /// <summary>
        /// BGMAssistant及びSEAssistantの管理者
        /// </summary>
        [field: SerializeField]
        public AudioAssistantsManager AudioAssistantsM { get; private set; } = null;

        private void Start()
        {
            Init();
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        private void Init()
        {
            InitAllManagers();
        }

        /// <summary>
        /// マネージャーを初期化する
        /// </summary>
        private void InitAllManagers()
        {
            if (FieldM && FieldM.isActiveAndEnabled)
            {
                FieldM.Init();

                if (ObjectsOnFieldM && ObjectsOnFieldM.isActiveAndEnabled)
                {
                    ObjectsOnFieldM.Init(FieldM.CurrentField);
                }

                FieldM.CurrentField.InitPositonsOfPlayer();
                FieldM.CurrentField.ReproduceEnemiesDefeatingState();
                FieldM.CurrentField.ReproduceGiftBoxeCollectionState();
            }

            if (BattleM && BattleM.isActiveAndEnabled)
            {
                BattleM.Init();
            }

            if (CameraM && CameraM.isActiveAndEnabled)
            {
                CameraM.Init();
            }

            if (EffectM && EffectM.isActiveAndEnabled)
            {
                EffectM.Init();
            }

            if (UIM && UIM.isActiveAndEnabled)
            {
                UIM.Init();
            }

            if (AudioAssistantsM && AudioAssistantsM.isActiveAndEnabled)
            {
                AudioAssistantsM.Init();
            }

            if (AdventureM && AdventureM.isActiveAndEnabled)
            {
                AdventureM.Init();
            }
        }
    }
}
