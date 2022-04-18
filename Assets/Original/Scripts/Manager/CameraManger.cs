using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

namespace DungeonRPG3D
{
    /// <summary>
    /// カメラの管理者
    /// </summary>
    public class CameraManger : ManagerBase
    {
        /// <summary>
        /// 汎用カメラ
        /// </summary>
        [field: SerializeField]
        public Camera CommonCamera { get; private set; } = null;

        /// <summary>
        /// 汎用仮想カメラ
        /// </summary>
        [field: SerializeField]
        public CinemachineVirtualCamera CommonVirtualCamera { get; private set; } = null;

        /// <summary>
        /// 冒険画面のカメラ
        /// </summary>
        [field: SerializeField]
        public Camera AdventureCamera { get; private set; } = null;

        /// <summary>
        /// 冒険画面の仮想カメラ
        /// </summary>
        [field: SerializeField]
        public CinemachineVirtualCamera AdventureVirtualCamera { get; private set; } = null;

        /// <summary>
        /// 戦闘画面のカメラ
        /// </summary>
        [field: SerializeField]
        public Camera BattleCamera { get; private set; } = null;

        /// <summary>
        /// 戦闘画面の仮想カメラ
        /// </summary>
        [field: SerializeField]
        public CinemachineVirtualCamera BattleVirtualCamera { get; private set; } = null;

        /// <summary>
        /// プレイヤーがダメージを受けたときの衝撃設定
        /// </summary>
        [field: SerializeField]
        public CinemachineImpulseSource PlayerDamageImpulse { get; private set; } = null;

        protected override void CacheComponents()
        {
            base.CacheComponents();

            PlayerDamageImpulse = GetComponent<CinemachineImpulseSource>();
        }

        public override void Init()
        {
            base.Init();

            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == Glossary.Scene.Game)
            {
                OnGameScene();
            }
        }

        /// <summary>
        /// ゲームシーンで行う処理
        /// </summary>
        private void OnGameScene()
        {
            Player player = ManagersMaster.Instance.ObjectsOnFieldM.Player;

            Transform adventureVirtualCameraTarget = player.AdventureVirtualCameraTarget;
            if (adventureVirtualCameraTarget)
            {
                // 冒険仮想カメラの追跡対象設定
                AdventureVirtualCamera.Follow = adventureVirtualCameraTarget;
                AdventureVirtualCamera.LookAt = adventureVirtualCameraTarget;
            }

            Transform battleVirtualCameraTarget = player.BattleVirtualCameraTarget;
            if (battleVirtualCameraTarget)
            {
                // 戦闘仮想カメラの追跡対象設定
                BattleVirtualCamera.Follow = battleVirtualCameraTarget;
            }
        }

        /// <summary>
        /// プレイヤー被ダメージの衝撃を生成する
        /// </summary>
        public void GeneratePlayerDamageImpulse()
        {
            PlayerDamageImpulse.GenerateImpulse();
        }
    }
}
