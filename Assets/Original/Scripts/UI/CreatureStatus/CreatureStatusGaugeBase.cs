using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物のステータス画像表示(ゲージタイプ)
    /// </summary>
    [RequireComponent(typeof(UIAnimation))]
    public abstract class CreatureStatusGaugeBase : CreatureStatusUIImageBase
    {
        /// <summary>
        /// 増減にかかる時間
        /// </summary>
        public static readonly float IncreaseAndDecreaseDuration = 0.2f;

        /// <summary>
        /// UIアニメーション
        /// </summary>
        [field: SerializeField]
        public UIAnimation SelfAnimation { get; protected set; } = null;

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        protected override void ChacheComponents()
        {
            base.ChacheComponents();

            SelfAnimation = GetComponent<UIAnimation>();
        }
    }
}
