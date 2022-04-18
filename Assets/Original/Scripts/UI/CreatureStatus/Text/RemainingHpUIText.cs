using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 残りHP表示UIテキスト
    /// </summary>
    public class RemainingHpUIText : CreatureStatusUITextBase
    {
        /// <summary>
        /// 表示すべき残りHP
        /// </summary>
        public int RemainingHpToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            int remainingHp = creatureBattle.RemainingHp;
            if (remainingHp != RemainingHpToBeDisplayed)
            {
                RemainingHpToBeDisplayed = remainingHp;

                TextToBeDisplayed = RemainingHpToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }

        protected override void UpdateDisplay()
        {
            base.UpdateDisplay();

            CreatureBattle creatureBattle = GetCreatureBattle();
            int remainingHp = creatureBattle.RemainingHp;
            int maxHp = creatureBattle.CurrentStatus.MaxHp;
            ChangeColor(remainingHp, maxHp);
        }
    }
}
