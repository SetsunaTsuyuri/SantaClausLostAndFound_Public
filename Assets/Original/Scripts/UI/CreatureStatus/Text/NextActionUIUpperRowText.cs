using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 行動表示UIテキスト(上の行)
    /// </summary>
    public class NextActionUIUpperRowText : CreatureStatusUITextBase
    {
        /// <summary>
        /// 表示すべき名前
        /// </summary>
        public string NameToBeDisplayed { get; private set; } = "";

        /// <summary>
        /// 表示すべきダメージ計算タイプ
        /// </summary>
        public ActionData.CalculationType CalculationToBeDisplayed { get; private set; } = ActionData.CalculationType.TrueDamage;

        /// <summary>
        /// 表示すべき属性
        /// </summary>
        public ActionData.AttributeType AttributeToBeDisplayed { get; private set; } = ActionData.AttributeType.None;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            ActionData actionData = creatureBattle.NextActionData;
            if (actionData == null)
            {
                return false;
            }

            bool result = false;

            if (NameToBeDisplayed != actionData.Name ||
                CalculationToBeDisplayed != actionData.Calculation ||
                AttributeToBeDisplayed != actionData.Attribute)
            {
                NameToBeDisplayed = actionData.Name;
                CalculationToBeDisplayed = actionData.Calculation;
                AttributeToBeDisplayed = actionData.Attribute;

                string text = NameToBeDisplayed;
                if (CalculationToBeDisplayed != ActionData.CalculationType.None ||
                    AttributeToBeDisplayed != ActionData.AttributeType.None)
                {
                    text += "【";
                    text += TextUtility.ToJapanese(CalculationToBeDisplayed);

                    if (CalculationToBeDisplayed != ActionData.CalculationType.None &&
                        AttributeToBeDisplayed != ActionData.AttributeType.None)
                    {
                        text += " / ";
                    }

                    text += TextUtility.ToJapanese(AttributeToBeDisplayed);
                    text += "】";
                }

                TextToBeDisplayed = text;

                result = true;
            }

            return result;
        }
    }
}
