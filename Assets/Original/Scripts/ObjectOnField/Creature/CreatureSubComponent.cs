using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物コンポーネントに付属するサブコンポーネント
    /// </summary>
    public abstract class CreatureSubComponent : MonoBehaviour
    {
        /// <summary>
        /// 生物
        /// </summary>
        [field: SerializeField, HideInInspector]
        public Creature Owner { get; protected set; } = null;

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        public virtual void ChacheComponents()
        {
            Owner = GetComponent<Creature>();
        }
    }
}
