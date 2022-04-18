using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 回収済みプレゼント箱の総数表示UIテキスト
    /// </summary>
    public class TotalCollectedBoxesUIText : UITextBase
    {
        /// <summary>
        /// 表示すべきプレゼント箱の総数
        /// </summary>
        public int TotalCollectedGiftBoxesToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            int totalCollectedGigtBoxes = GameController.Instance.TotalCollectedGiftBoxes;
            if (totalCollectedGigtBoxes != TotalCollectedGiftBoxesToBeDisplayed)
            {
                TotalCollectedGiftBoxesToBeDisplayed = totalCollectedGigtBoxes;

                StringTobeDisplayed = TotalCollectedGiftBoxesToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }

    }
}
