using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 現在のフィールドレベル表示UIテキスト
    /// </summary>
    public class CurrentFieldLevelUIText : UITextBase
    {
        /// <summary>
        /// 表示すべきフィールドレベル
        /// </summary>
        public int CurrentFieldLevelToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            int currentFieldLevel = GameController.Instance.CurrentFieldLevel;
            if (currentFieldLevel != CurrentFieldLevelToBeDisplayed)
            {
                CurrentFieldLevelToBeDisplayed = currentFieldLevel;

                string text = (CurrentFieldLevelToBeDisplayed + 1).ToString() + Glossary.UI.CurrentFiledLevelSuffix;
                StringTobeDisplayed = text;

                result = true;
            }

            return result;
        }

    }
}
