using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// 浮かぶ表示
    /// </summary>
    public class FloatDisplay : UIAnimationAutomatically
    {
        /// <summary>
        /// 経験値取得表示の設定
        /// </summary>
        [field: SerializeField]
        public FloatDisplaySetting Settings { get; private set; } = null;

        /// <summary>
        /// 自身のテキスト
        /// </summary>
        [field: SerializeField]
        public Text SelfText { get; private set; } = null;

        protected override void ChacheComponent()
        {
            base.ChacheComponent();

            SelfText = GetComponentInChildren<Text>();
        }

        /// <summary>
        /// テキストの文字列を設定する
        /// </summary>
        /// <param name="newString">新しいテキスト</param>
        public void SetText(string newString)
        {
            SelfText.text = newString;
        }

        /// <summary>
        /// テキストの文字列を設定する
        /// </summary>
        /// <param name="value">値</param>
        public void SetText(int value)
        {
            SetText(value.ToString());
        }

        /// <summary>
        /// テキストの文字列を設定する
        /// </summary>
        /// <param name="prefix">接頭辞</param>
        /// <param name="value">値</param>
        public void SetText(string prefix, int value)
        {
            string newString = "";
            newString += prefix;
            newString += value.ToString();

            SetText(newString);
        }

        protected override void OnEnableInnner()
        {
            SelfUIAnimation.MoveFadeAndDeactivate(Settings);
        }

        /// <summary>
        /// 座標を初期化する
        /// </summary>
        public void InitPosition()
        {
            SelfUIAnimation.InitPosition();
        }

        /// <summary>
        /// 座標を設定する
        /// </summary>
        /// <param name="newPosition">座標</param>
        public void SetPosition(Vector3 newPosition)
        {
            SelfUIAnimation.SelfTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, newPosition);
        }
    }
}
