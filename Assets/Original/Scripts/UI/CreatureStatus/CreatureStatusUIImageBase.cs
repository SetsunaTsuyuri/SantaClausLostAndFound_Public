using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物ステータス表示UI画像
    /// </summary>
    public abstract class CreatureStatusUIImageBase : CreatureStatusUIBase
    {
        /// <summary>
        /// UI画像
        /// </summary>
        [field: SerializeField]
        public Image SelfImage { get; protected set; } = null;

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        protected override void ChacheComponents()
        {
            base.ChacheComponents();

            SelfImage = GetComponent<Image>();
        }
    }
}