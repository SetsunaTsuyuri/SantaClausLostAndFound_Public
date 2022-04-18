using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 行動を伴う戦闘アイテムのデータ
    /// </summary>
    public abstract class ActionItemData : BattleItemData
    {
        /// <summary>
        /// チャージタイム
        /// </summary>
        [field: SerializeField]
        public int ChargeTime { get; private set; } = 10;
    }
}
