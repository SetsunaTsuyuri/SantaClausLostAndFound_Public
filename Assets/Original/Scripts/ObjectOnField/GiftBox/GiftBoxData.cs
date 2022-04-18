using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレゼント箱のデータ
    /// </summary>
    [System.Serializable]
    public class GiftBoxData
    {
        /// <summary>
        /// 3Dモデル
        /// </summary>
        [field: SerializeField]
        public GameObject Model { get; private set; } = null;

        /// <summary>
        /// アイテム
        /// </summary>
        [field: SerializeField]
        public Item Item { get; private set; } = null;

        /// <summary>
        /// 経験値
        /// </summary>
        [field: SerializeField]
        public int ExperiencePoint { get; private set; } = 10;
    }
}
