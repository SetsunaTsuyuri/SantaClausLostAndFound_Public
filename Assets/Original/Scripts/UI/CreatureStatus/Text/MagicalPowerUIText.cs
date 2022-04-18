using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 魔力の表示UIテキスト
    /// </summary>
    public class MagicalPowerUIText : CreatureStatusUITextBase
    {
        /// <summary>
        /// 表示すべき魔力
        /// </summary>
        public int MagicalPowerToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            int magicalPower = creatureBattle.CurrentStatus.MagicalPower;
            if (magicalPower != MagicalPowerToBeDisplayed)
            {
                MagicalPowerToBeDisplayed = magicalPower;

                TextToBeDisplayed = MagicalPowerToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
