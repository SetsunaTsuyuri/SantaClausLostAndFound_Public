using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

namespace DungeonRPG3D
{
    /// <summary>
    /// ステージデータ
    /// </summary>
    [CreateAssetMenu]
    public class StageData : ScriptableObject
    {
        /// <summary>
        /// 名前
        /// </summary>
        [field: SerializeField]
        public string Name { get; private set; } = "NO_NAME";

        /// <summary>
        /// 説明
        /// </summary>
        [field: SerializeField]
        public string Description { get; private set; } = "NO_DISCRIPTION";

        /// <summary>
        /// フィールドの配列
        /// </summary>
        [field: SerializeField]
        public Field[] Fields { get; private set; } = null;
    }
}
