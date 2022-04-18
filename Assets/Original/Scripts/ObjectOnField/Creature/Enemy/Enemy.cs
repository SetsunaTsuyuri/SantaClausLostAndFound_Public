using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 敵
    /// </summary>
    [RequireComponent(typeof(EnemyMove))]
    [RequireComponent(typeof(EnemyMoveAI))]
    [RequireComponent(typeof(EnemyBattle))]
    [RequireComponent(typeof(EnemyBattleAI))]
    public class Enemy : Creature
    {
        protected override void ChacheComponents()
        {
            base.ChacheComponents();

        }

        public override CreatureData GetData()
        {
            return ManagersMaster.Instance.ObjectsOnFieldM.EnemyDataList.Data[ID];
        }

        /// <summary>
        /// プレイヤーに触れられた場合の処理
        /// </summary>
        public override void OnTouchedByPlayer()
        {
            // 戦闘態勢に移行する
            ChangeState(State.PreparingForBattle);
        }

        public override void Init()
        {
            base.Init();

            InitBattleVirtualCameraTarget();
        }

        /// <summary>
        /// 戦闘用仮想カメラの追跡対象を初期化する
        /// </summary>
        private void InitBattleVirtualCameraTarget()
        {
            Vector3 newPosition = BattleVirtualCameraTarget.position;
            newPosition.y += GetEnemyData().AimedTargetOffsetY;
            BattleVirtualCameraTarget.position = newPosition;
        }

        /// <summary>
        /// 既に倒されていた場合の処理
        /// </summary>
        public void OnBeingDefeatedAlready()
        {
            ChangeState(State.Dead);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 自身の敵データを取得する
        /// </summary>
        /// <returns>敵データ</returns>
        public EnemyData GetEnemyData()
        {
            return GetData() as EnemyData;
        }

        /// <summary>
        /// 経験値を取得する
        /// </summary>
        /// <returns>経験値</returns>
        public int GetExperiece()
        {
            float tempExp = GetEnemyData().Experience;
            int level = SelfBattle.CurrentLevel;
            float coefficient = ManagersMaster.Instance.ObjectsOnFieldM.PlayerSetting.ExperienceCoefficient;

            for (int i = CreatureBattle.MinLevel; i < level; i++)
            {
                tempExp *= coefficient;
            }

            int exeperience = Mathf.FloorToInt(tempExp);
            return exeperience;
        }
    }
}
