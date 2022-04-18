using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物の情報表示UI
    /// </summary>
    public abstract class CreatureStatusUIBase : UIDisplayBase
    {
        /// <summary>
        /// 表示対象の種類
        /// </summary>
        public enum TargetType
        {
            Player = 0,
            Enemy = 1
        }

        /// <summary>
        /// 表示対象
        /// </summary>
        [field: SerializeField]
        public TargetType Target { get; protected set; } = TargetType.Player;

        private void Reset()
        {
            ChacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        protected virtual void ChacheComponents() { }

        /// <summary>
        /// 生物(戦闘)を取得する
        /// </summary>
        /// <returns>生物(戦闘)</returns>
        protected CreatureBattle GetCreatureBattle()
        {
            CreatureBattle creatureBattle = null;

            switch (Target)
            {
                case TargetType.Player:
                    creatureBattle = ManagersMaster.Instance.BattleM.PlayerBattle;
                    break;

                case TargetType.Enemy:
                    creatureBattle = ManagersMaster.Instance.BattleM.EnemyBattle;
                    break;
            }

            return creatureBattle;
        }
    }
}

