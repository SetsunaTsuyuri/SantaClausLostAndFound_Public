using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 素早さの表示UIテキスト
    /// </summary>
    public class AgilityUIText : CreatureStatusUITextBase
    {
        /// <summary>
        /// 表示すべき素早さ
        /// </summary>
        public int AgilityToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            int agility = creatureBattle.CurrentStatus.Agility;
            if (agility != AgilityToBeDisplayed)
            {
                AgilityToBeDisplayed = agility;

                TextToBeDisplayed = AgilityToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
