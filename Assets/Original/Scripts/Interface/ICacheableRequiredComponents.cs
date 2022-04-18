using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 必要なコンポーネントをキャッシュする
    /// </summary>
    public interface ICacheableRequiredComponents
    {
        /// <summary>
        /// 必要なコンポーネントをキャッシュする
        /// </summary>
        void CacheRequiredComponents();
    }
}
