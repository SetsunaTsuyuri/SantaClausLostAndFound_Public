using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレゼント箱の総数表示UIテキスト
    /// </summary>
    public class TotalGiftBoxesUIText : UITextBase
    {
        /// <summary>
        /// 表示すべきプレゼント箱の総数
        /// </summary>
        public int TotalGiftBoxesToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            int totalGigtBoxes = GameController.Instance.TotalGiftBoxes;
            if (totalGigtBoxes != TotalGiftBoxesToBeDisplayed)
            {
                TotalGiftBoxesToBeDisplayed = totalGigtBoxes;

                StringTobeDisplayed = TotalGiftBoxesToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
