using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 回収済みのプレゼント箱の数表示UIテキスト
    /// </summary>
    public class CollectedGiftBoxesUIText : UITextBase
    {
        /// <summary>
        /// 表示すべき回収済みプレゼントの数
        /// </summary>
        public int CollectedGiftBoxesTobeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            int collected = ManagersMaster.Instance.ObjectsOnFieldM.CountCollectedGiftBoxes();
            if (CollectedGiftBoxesTobeDisplayed != collected)
            {
                CollectedGiftBoxesTobeDisplayed = collected;

                StringTobeDisplayed = CollectedGiftBoxesTobeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
