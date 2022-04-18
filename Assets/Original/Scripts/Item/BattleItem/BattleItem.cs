using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 戦闘アイテム
    /// </summary>
    public abstract class BattleItem : Item
    {
        /// <summary>
        /// 初期化する
        /// </summary>
        public virtual void Init() { }

        /// <summary>
        /// 使用できるか
        /// </summary>
        /// <returns></returns>
        public virtual bool CanUse()
        {
            // 非行動アイテムは選択できない
            if (this is NonActionItem)
            {
                return false;
            }

            bool result = true;

            return result;
        }
    }
}
