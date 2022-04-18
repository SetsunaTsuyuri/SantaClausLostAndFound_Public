using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 名前表示UIテキスト
    /// </summary>
    public class NameUIText : CreatureStatusUITextBase
    {
        /// <summary>
        /// 表示すべき名前
        /// </summary>
        public string NameToBeDisplayed { get; private set; } = "";

        /// <summary>
        /// 選択中のアイテムを使用可能か
        /// </summary>
        public bool CanUseSelectedItem { get; private set; } = false;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            string name = creatureBattle.GetData().Name;
            if (NameToBeDisplayed != name)
            {
                NameToBeDisplayed = name;

                TextToBeDisplayed = NameToBeDisplayed;

                result = true;
            }

            return result;

        }
    }
}
