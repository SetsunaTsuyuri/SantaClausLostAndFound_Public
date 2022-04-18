using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 文字関連の便利クラス
/// </summary>
namespace DungeonRPG3D
{
    public class TextUtility
    {
        public static string ToJapanese(ActionData.CalculationType calculation)
        {
            string text = "";

            switch (calculation)
            {
                case ActionData.CalculationType.None:
                    text = Glossary.Calculation.None;
                    break;

                case ActionData.CalculationType.TrueDamage:
                    text = Glossary.Calculation.TrueDamage;
                    break;

                case ActionData.CalculationType.Physical:
                    text = Glossary.Calculation.Physical;
                    break;

                case ActionData.CalculationType.Magical:
                    text = Glossary.Calculation.Magical;
                    break;
            }

            return text;
        }

        /// <summary>
        /// 属性を日本語文字列に変換する
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns></returns>
        public static string ToJapanese(ActionData.AttributeType attribute)
        {
            string text = "";

            switch (attribute)
            {
                case ActionData.AttributeType.None:
                    text = Glossary.Attribute.None;
                    break;

                case ActionData.AttributeType.Slashing:
                    text = Glossary.Attribute.Slashing;
                    break;

                case ActionData.AttributeType.Stabbing:
                    text = Glossary.Attribute.Stabbing;
                    break;

                case ActionData.AttributeType.Blow:
                    text = Glossary.Attribute.Blow;
                    break;

                case ActionData.AttributeType.Fire:
                    text = Glossary.Attribute.Fire;
                    break;

                case ActionData.AttributeType.Ice:
                    text = Glossary.Attribute.Ice;
                    break;

                case ActionData.AttributeType.Thunder:
                    text = Glossary.Attribute.Thunder;
                    break;
            }

            return text;
        }
    }

}
