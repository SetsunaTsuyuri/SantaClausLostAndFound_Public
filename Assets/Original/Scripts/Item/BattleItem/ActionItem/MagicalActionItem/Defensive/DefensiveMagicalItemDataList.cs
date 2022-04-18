using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 魔法攻撃アイテムデータの配列
    /// </summary>
    [CreateAssetMenu(menuName = "Item Data/Defensive Magical Item Data List")]
    public class DefensiveMagicalItemDataList : ScriptableObject
    {
        /// <summary>
        /// 魔法の杖のデータ
        /// </summary>
        [field: SerializeField]
        public DefensiveMagicalItemData[] MagicStaffDataList { get; private set; }
    }
}