using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレゼント箱のデータリスト
    /// </summary>
    [CreateAssetMenu]
    public class GiftBoxDataList : ScriptableObject
    {
        /// <summary>
        /// プレゼント箱のデータ
        /// </summary>
        [field: SerializeField]
        public GiftBoxData[] Data { get; private set; } = { };
    }
}
