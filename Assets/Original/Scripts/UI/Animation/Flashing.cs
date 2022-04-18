using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 点滅
    /// </summary>
    [RequireComponent(typeof(UIAnimation))]
    public class Flashing : MonoBehaviour
    {
        /// <summary>
        /// 自身のアニメーション
        /// </summary>
        [field: SerializeField]
        public UIAnimation SelfAnimation { get; private set; } = null;

        /// <summary>
        /// 点滅の設定
        /// </summary>
        [field: SerializeField]
        public FlashingSetting FlashingSetting { get; private set; } = null;

        private void Reset()
        {
            CacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        private void CacheComponents()
        {
            SelfAnimation = GetComponent<UIAnimation>();
        }

        /// <summary>
        /// 点滅する(コルーチン)
        /// </summary>
        /// <returns></returns>
        public IEnumerator Flash()
        {
            SelfAnimation.Flash(FlashingSetting);
            yield return new WaitForSeconds(FlashingSetting.WaitingTime);
        }
    }

}
