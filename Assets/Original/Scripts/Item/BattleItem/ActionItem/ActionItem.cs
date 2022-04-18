using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 行動を伴う戦闘アイテム
    /// </summary>
    public abstract class ActionItem : BattleItem
    {
        /// <summary>
        /// 行動データを取得する
        /// </summary>
        /// <returns>行動データ</returns>
        public abstract ActionData GetActionData();
    }
}
