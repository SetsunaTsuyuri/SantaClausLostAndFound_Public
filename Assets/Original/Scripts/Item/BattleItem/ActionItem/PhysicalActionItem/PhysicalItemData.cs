using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 物理系の行動を伴う戦闘アイテムのデータ
    /// </summary>
    public abstract class PhysicalItemData : ActionItemData
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
    }
}
