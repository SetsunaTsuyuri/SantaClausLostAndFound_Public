using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 管理者クラスの原型
    /// </summary>
    public abstract class ManagerBase : MonoBehaviour
    {
        private void Reset()
        {
            CacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        protected virtual void CacheComponents() { }

        private void Start()
        {
            StartInner();
        }

        private void Update()
        {
            UpdateInner();
        }

        private void LateUpdate()
        {
            LateUpdateInnner();
        }

        /// <summary>
        /// Updateが最初に呼ばれる前に1度だけ行う処理
        /// </summary>
        protected virtual void StartInner() { }

        /// <summary>
        /// 1フレームに1度行う処理
        /// </summary>
        protected virtual void UpdateInner() { }

        /// <summary>
        /// 1フレームに1度、最後に実行する処理
        /// </summary>
        protected virtual void LateUpdateInnner() { }

        /// <summary>
        /// 初期化する
        /// </summary>
        public virtual void Init() { }
    }
}
