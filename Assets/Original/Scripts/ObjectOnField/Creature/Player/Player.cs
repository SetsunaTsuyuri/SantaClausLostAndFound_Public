using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    [RequireComponent(typeof(PlayerMove))]
    [RequireComponent(typeof(PlayerBattle))]
    public class Player : Creature
    {
        /// <summary>
        /// 経験値
        /// </summary>
        public int Experience { get; protected set; } = 0;

        /// <summary>
        /// 冒険カメラに追跡させる対象
        /// </summary>
        [field: SerializeField, HideInInspector]
        public Transform AdventureVirtualCameraTarget { get; private set; } = null;

        /// <summary>
        /// 初期化されたか
        /// </summary>
        public bool HasInitialized { get; private set; } = false;

        protected override void ChacheComponents()
        {
            base.ChacheComponents();

            AdventureVirtualCameraTarget = SelfTransform.Find(Glossary.Camera.AdventureVirtualCameraTarget);
        }

        public override void Init()
        {
            if (HasInitialized)
            {
                return;
            }

            base.Init();

            HasInitialized = true;
        }

        protected override void UpdateInner()
        {
            base.UpdateInner();
        }

        public override CreatureData GetData()
        {
            return ManagersMaster.Instance.ObjectsOnFieldM.PlayerDataList.Data[ID];
        }

        /// <summary>
        /// 経験値を得る
        /// </summary>
        /// <param name="experience">経験値</param>
        public void GainExeperience(int experience)
        {
            // 経験値獲得演出
            EffectManager effectManager = ManagersMaster.Instance.EffectM;
            effectManager.PlayGainingExperienceDirection(experience);

            // 経験値増加
            Experience += experience;

            // レベル更新
            UpdateLevel();
        }

        /// <summary>
        /// 経験値を設定する
        /// </summary>
        /// <param name="newExperience">経験値</param>
        public void SetExperience(int newExperience)
        {
            // 経験値設定
            Experience = newExperience;

            // レベル設定
            UpdateLevel();
        }

        /// <summary>
        /// レベルを更新する
        /// </summary>
        public void UpdateLevel()
        {
            int newLevel = CalculationUtility.ToLevel(Experience);
            SetLevel(newLevel);
            SelfBattle.UpdateStatus();
        }

        /// <summary>
        /// レベルを設定する
        /// </summary>
        /// <param name="newLevel">新しいレベル</param>
        public void SetLevel(int newLevel)
        {
            SelfBattle.SetLevel(newLevel);
        }
    }
}
