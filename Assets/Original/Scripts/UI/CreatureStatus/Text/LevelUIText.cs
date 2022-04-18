using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// レベルの表示UIテキスト
    /// </summary>
    public class LevelUIText : CreatureStatusUITextBase
    {
        /// <summary>
        /// レベルの桁数
        /// </summary>
        public static readonly int LevelDigits = 2;

        /// <summary>
        /// 表示すべきレベル
        /// </summary>
        public int LevelToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            int level = creatureBattle.CurrentLevel;
            if (LevelToBeDisplayed != level)
            {
                LevelToBeDisplayed = level;

                TextToBeDisplayed = LevelToBeDisplayed.ToString().PadLeft(LevelDigits);

                result = true;
            }

            return result;
        }
    }
}

