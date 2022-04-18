using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// フィールド上のプレゼント箱の最大数表示UIテキスト
    /// </summary>
    public class MaxGiftBoxesUIText : UITextBase
    {
        public int MaxGiftBoxesToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            int maxGiftBoxes = ManagersMaster.Instance.ObjectsOnFieldM.CountGiftBoxes();
            if (MaxGiftBoxesToBeDisplayed != maxGiftBoxes)
            {
                MaxGiftBoxesToBeDisplayed = maxGiftBoxes;

                StringTobeDisplayed = MaxGiftBoxesToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}

