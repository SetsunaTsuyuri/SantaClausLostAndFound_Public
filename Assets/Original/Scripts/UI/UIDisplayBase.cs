using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// UI表示を変化させるクラス
    /// </summary>
    public abstract class UIDisplayBase : MonoBehaviour
    {
        private void LateUpdate()
        {
            LateUpdateInner();
        }

        /// <summary>
        /// 毎フレーム最後に行う処理
        /// </summary>
        protected virtual void LateUpdateInner()
        {
            // 値が変化した場合
            if (CheckValueHasChanged())
            {
                // UI表示更新
                UpdateDisplay();
            }
        }

        /// <summary>
        /// 監視すべき変数の値が変化したか
        /// </summary>
        /// <returns></returns>
        protected abstract bool CheckValueHasChanged();

        /// <summary>
        /// 表示を更新する
        /// </summary>
        protected abstract void UpdateDisplay();
    }
}
