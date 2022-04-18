using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// UIテキスト表示
    /// </summary>
    public abstract class UITextBase : UIDisplayBase
    {
        /// <summary>
        /// UIテキスト
        /// </summary>
        [field: SerializeField]
        public Text SelfText { get; protected set; } = null;

        /// <summary>
        /// 表示すべき文字列
        /// </summary>
        public string StringTobeDisplayed { get; protected set; } = "";

        private void Reset()
        {
            ChacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        protected virtual void ChacheComponents()
        {
            SelfText = GetComponent<Text>();
        }

        protected override void UpdateDisplay()
        {
            SelfText.text = StringTobeDisplayed;
        }
    }
}
