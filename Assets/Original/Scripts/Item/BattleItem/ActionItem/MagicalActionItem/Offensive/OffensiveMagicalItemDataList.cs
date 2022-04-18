using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 魔法攻撃アイテムデータの配列
    /// </summary>
    [CreateAssetMenu(menuName = "Item Data/Offensive Magical Item Data List")]
    public class OffensiveMagicalItemDataList : ScriptableObject
    {
        /// <summary>
        /// 魔導書のデータ
        /// </summary>
        [field: SerializeField]
        public OffensiveMagicalItemData[] GrimoierDataList { get; private set; }

        /// <summary>
        /// 竜石のデータ
        /// </summary>
        [field: SerializeField]
        public OffensiveMagicalItemData[] DragonGemDataList { get; private set; }
    }
}
