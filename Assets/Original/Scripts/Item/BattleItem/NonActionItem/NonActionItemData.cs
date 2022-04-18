using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 非行動戦闘アイテムのデータ
    /// </summary>
    [System.Serializable]
    public class NonActionItemData : BattleItemData
    {
        /// <summary>
        /// 攻撃力の補正値
        /// </summary>
        [field: SerializeField]
        public int OffensivePowerCorrection { get; private set; } = 10;

        /// <summary>
        /// 守備力の補正値
        /// </summary>
        [field: SerializeField]
        public int DefensivePowerCorrection { get; private set; } = 10;

        /// <summary>
        /// 魔力の補正値
        /// </summary>
        [field: SerializeField]
        public int MagicalPowerCorrection { get; private set; } = 10;

        /// <summary>
        /// 素早さの補正値
        /// </summary>
        [field: SerializeField]
        public int AgilityCorrection { get; private set; } = 10;
    }
}
