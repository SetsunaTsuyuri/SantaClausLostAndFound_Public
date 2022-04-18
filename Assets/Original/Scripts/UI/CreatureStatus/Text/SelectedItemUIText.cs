using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 選択中のアイテム表示UIテキスト
    /// </summary>
    public class SelectedItemUIText : CreatureStatusUITextBase
    {
        /// <summary>
        /// アイテムを選んでいない場合に表示される文字列
        /// </summary>
        public static readonly string NotSelection = "-";

        /// <summary>
        /// 表示すべき名前
        /// </summary>
        public string NameToBeDisplayed { get; private set; } = "";

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            if (creatureBattle.SelectedItemIndex.HasValue)
            {
                BattleItem battleItem = creatureBattle.GetSelectedItem();
                string name = battleItem.GetData().Name;
                if (NameToBeDisplayed != name)
                {
                    NameToBeDisplayed = name;

                    TextToBeDisplayed = NameToBeDisplayed;

                    result = true;
                }
            }
            else if (NameToBeDisplayed != NotSelection)
            {
                NameToBeDisplayed = NotSelection;

                TextToBeDisplayed = NameToBeDisplayed;

                result = true;
            }

            return result;
        }
    }
}
