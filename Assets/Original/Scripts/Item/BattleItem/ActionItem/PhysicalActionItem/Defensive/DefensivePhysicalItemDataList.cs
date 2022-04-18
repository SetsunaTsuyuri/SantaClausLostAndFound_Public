using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 物理防御アイテムデータの配列
    /// </summary>
    [CreateAssetMenu(menuName = "Item Data/Defensive Physical Item Data List")]
    public class DefensivePhysicalItemDataList : ScriptableObject
    {
        /// <summary>
        /// 盾のデータ
        /// </summary>
        [field: SerializeField]
        public DefensivePhysicalItemData[] ShieldData { get; private set; }
    }
}
