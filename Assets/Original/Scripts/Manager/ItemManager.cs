using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// アイテムの管理者
    /// </summary>
    public class ItemManager : ManagerBase
    {
        /// <summary>
        /// 物理攻撃アイテムのデータ
        /// </summary>
        [field: SerializeField]
        public OffensivePhysicalItemDataList OffensivePhysicalItemDataList { get; private set; } = null;

        /// <summary>
        /// 物理防御アイテムのデータ
        /// </summary>
        [field: SerializeField]
        public DefensivePhysicalItemDataList DefensivePhysicalItemDataList { get; private set; } = null;

        /// <summary>
        /// 魔法攻撃アイテムのデータ
        /// </summary>
        [field: SerializeField]
        public OffensiveMagicalItemDataList OffensiveMagicalItemDataList { get; private set; } = null;

        /// <summary>
        /// 魔法防御アイテムのデータ
        /// </summary>
        [field: SerializeField]
        public DefensiveMagicalItemDataList DefensiveMagicalItemDataList { get; private set; } = null;

        /// <summary>
        /// 非行動アイテムのデータ
        /// </summary>
        [field: SerializeField]
        public NonActionItemDataList NonActionItemDataList { get; private set; } = null;
    }
}
