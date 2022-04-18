using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// ステージ名の表示UIテキスト
    /// </summary>
    public class StageNameUIText : UITextBase
    {
        /// <summary>
        /// 表示すべきステージ名
        /// </summary>
        public string StageNameToBeDisplayed { get; private set; } = "";

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            string stageName = GameController.Instance.GetSelectedStageData().Name;
            if (stageName != StageNameToBeDisplayed)
            {
                StageNameToBeDisplayed = stageName;

                StringTobeDisplayed = StageNameToBeDisplayed;

                result = true;
            }

            return result;
        }
    }
}
