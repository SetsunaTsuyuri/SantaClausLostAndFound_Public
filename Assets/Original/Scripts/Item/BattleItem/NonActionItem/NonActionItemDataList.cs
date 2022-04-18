using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 非行動戦闘アイテムデータの配列
    /// </summary>
    [CreateAssetMenu(menuName = "Item Data/Non Action Item Data List")]
    public class NonActionItemDataList : ScriptableObject
    {
        /// <summary>
        /// データ
        /// </summary>
        [field: SerializeField]
        public NonActionItemData[] Data { get; private set; }
    }
}
