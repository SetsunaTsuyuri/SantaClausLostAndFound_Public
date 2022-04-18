using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 敵のデータを纏めた配列
    /// </summary>
    [CreateAssetMenu(menuName = "Creature Data/Enemy Data List")]
    public class EnemyDataList : ScriptableObject
    {
        /// <summary>
        /// 敵データの配列
        /// </summary>
        [field: SerializeField]
        public EnemyData[] Data { get; private set; }
    }
}
