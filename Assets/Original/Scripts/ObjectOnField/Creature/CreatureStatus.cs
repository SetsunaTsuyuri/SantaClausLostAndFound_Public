using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物のステータス
    /// </summary>
    [System.Serializable]
    public struct CreatureStatus
    {
        /// <summary>
        /// 最大HP
        /// </summary>
        public int MaxHp;

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int OffensivePower;

        /// <summary>
        /// 守備力
        /// </summary>
        public int DefensivePower;

        /// <summary>
        /// 魔力
        /// </summary>
        public int MagicalPower;

        /// <summary>
        /// 素早さ
        /// </summary>
        public int Agility;
    }
}
