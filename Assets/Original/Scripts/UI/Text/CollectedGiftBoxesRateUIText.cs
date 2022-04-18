using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレゼント箱の回収率表示テキスト
    /// </summary>
    public class CollectedGiftBoxesRateUIText : UITextBase
    {
        /// <summary>
        /// 表示すべき回収率
        /// </summary>
        public float CollectedGiftBoxesRateToBeDisplayed { get; private set; } = -1.0f;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            float collectedGiftBoxesRate = GameController.Instance.CollectedGiftBoxesRate;
            if (collectedGiftBoxesRate != CollectedGiftBoxesRateToBeDisplayed)
            {
                CollectedGiftBoxesRateToBeDisplayed = collectedGiftBoxesRate;

                string newText = Mathf.Floor(collectedGiftBoxesRate * 100) + "%";

                StringTobeDisplayed = newText;

                result = true;
            }

            return result;

        }
    }
}
