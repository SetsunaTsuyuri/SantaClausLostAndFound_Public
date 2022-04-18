using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物ステータス表示UIテキスト
    /// </summary>
    public abstract class CreatureStatusUITextBase : CreatureStatusUIBase
    {
        /// <summary>
        /// 赤色に表示される割合
        /// </summary>
        public static readonly float Red = 0.2f;

        /// <summary>
        /// 黄色に表示される割合
        /// </summary>
        public static readonly float Yellow = 0.5f;

        /// <summary>
        /// 黄緑色に表示される割合
        /// </summary>
        public static readonly float YellowGreen = 1.0f;

        /// <summary>
        /// 表示すべきテキスト
        /// </summary>
        public string TextToBeDisplayed { get; protected set; } = "";

        /// <summary>
        /// UIテキスト
        /// </summary>
        [field: SerializeField]
        public Text SelfText { get; protected set; } = null;

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        protected override void ChacheComponents()
        {
            base.ChacheComponents();

            SelfText = GetComponent<Text>();
        }

        /// <summary>
        /// 現在値と最大値の割合に応じてテキストの色を変更する
        /// </summary>
        /// <param name="current">現在値</param>
        /// <param name="max">最大値</param>
        protected void ChangeColor(int current, int max)
        {
            float percentage = CalculationUtility.GetPercentage(current, max);

            if (percentage <= Red)
            {
                SelfText.color = Color.red;
            }
            else if (percentage <= Yellow)
            {
                SelfText.color = Color.yellow;
            }
            else if (percentage > YellowGreen)
            {
                SelfText.color = Color.yellow;
            }
            else
            {
                SelfText.color = Color.white;
            }
        }

        /// <summary>
        /// 表示を更新する
        /// </summary>
        protected override void UpdateDisplay()
        {
            SelfText.text = TextToBeDisplayed;
        }
    }
}
