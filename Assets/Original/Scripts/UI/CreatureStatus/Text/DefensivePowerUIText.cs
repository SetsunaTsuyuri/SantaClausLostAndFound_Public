using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 守備力の表示UIテキスト
    /// </summary>
    public class DefensivePowerUIText : CreatureStatusUITextBase
    {
        /// <summary>
        /// 表示すべき守備力
        /// </summary>
        public int DefensivePowerToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            int defensivePower = creatureBattle.CurrentStatus.DefensivePower;
            if (defensivePower != DefensivePowerToBeDisplayed)
            {
                DefensivePowerToBeDisplayed = defensivePower;

                TextToBeDisplayed = DefensivePowerToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
