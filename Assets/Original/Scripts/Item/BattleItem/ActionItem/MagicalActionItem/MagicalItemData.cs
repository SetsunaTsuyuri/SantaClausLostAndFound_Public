using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 魔法系の行動を伴う戦闘アイテムのデータ
    /// </summary>
    public abstract class MagicalItemData : ActionItemData
    {
        /// <summary>
        /// 魔力の補正値
        /// </summary>
        [field: SerializeField]
        public int MagicalPowerCorrection { get; private set; } = 10;
    }
}
