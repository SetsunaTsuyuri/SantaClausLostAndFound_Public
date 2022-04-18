using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 自動的に動くUIアニメーション
    /// </summary>
    [RequireComponent(typeof(UIAnimation))]
    public abstract class UIAnimationAutomatically : MonoBehaviour
    {
        /// <summary>
        /// UIアニメーション
        /// </summary>
        [field: SerializeField, HideInInspector]
        public UIAnimation SelfUIAnimation { get; private set; }

        private void Reset()
        {
            ChacheComponent();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        protected virtual void ChacheComponent()
        {
            SelfUIAnimation = GetComponent<UIAnimation>();
        }

        private void OnEnable()
        {
            OnEnableInnner();
        }

        /// <summary>
        /// このオブジェクトが有効でアクティブになったとき、最初に行う処理
        /// </summary>
        protected abstract void OnEnableInnner();

        private void Update()
        {
            UpdateInner();
        }

        /// <summary>
        /// 毎フレーム行う処理
        /// </summary>
        protected virtual void UpdateInner() { }
    }
}
