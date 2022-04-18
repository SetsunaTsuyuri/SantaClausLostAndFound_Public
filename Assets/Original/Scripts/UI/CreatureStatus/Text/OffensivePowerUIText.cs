using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 攻撃力の表示UIテキスト
    /// </summary>
    public class OffensivePowerUIText : CreatureStatusUITextBase
    {
        /// <summary>
        /// 表示すべき攻撃力
        /// </summary>
        public int OffensivePowerToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            int offensivePower = creatureBattle.CurrentStatus.OffensivePower;
            if (offensivePower != OffensivePowerToBeDisplayed)
            {
                OffensivePowerToBeDisplayed = offensivePower;

                TextToBeDisplayed = OffensivePowerToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
