using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレイヤーの最大HP表示UIテキスト
    /// </summary>
    public class MaxHpUIText : CreatureStatusUITextBase
    {
        /// <summary>
        /// 表示すべき最大HPの値
        /// </summary>
        public int MaxHpToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            int maxHp = creatureBattle.CurrentStatus.MaxHp;
            if (maxHp != MaxHpToBeDisplayed)
            {
                MaxHpToBeDisplayed = maxHp;

                TextToBeDisplayed = MaxHpToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
