using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレイヤーの戦闘
    /// </summary>
    public class PlayerBattle : CreatureBattle
    {
        /// <summary>
        /// 最も番号が若い行動アイテムを選択した状態にする(なければnull)
        /// </summary>
        public void SelectTheActionItemWithTheLowestNumber()
        {
            int? index = null;

            for (int i = 0; i < EquippedItems.Count; i++)
            {
                if (EquippedItems[i] is ActionItem)
                {
                    index = i;
                    break;
                }
            }

            ChangeItemInUse(index);
        }
    }
}
