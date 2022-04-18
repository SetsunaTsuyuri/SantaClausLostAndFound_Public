using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 物理攻撃アイテムデータの配列
    /// </summary>
    [CreateAssetMenu(menuName = "Item Data/Offensive Physical Item Data List")]
    public class OffensivePhysicalItemDataList : ScriptableObject
    {
        /// <summary>
        /// 剣のデータ
        /// </summary>
        [field: SerializeField]
        public OffensivePhysicalItemData[] SwordData { get; private set; }

        /// <summary>
        /// 槍のデータ
        /// </summary>
        [field: SerializeField]
        public OffensivePhysicalItemData[] SpearData { get; private set; }

        /// <summary>
        /// 槌のデータ
        /// </summary>
        [field: SerializeField]
        public OffensivePhysicalItemData[] HammerData { get; private set; }

        /// <summary>
        /// 爪のデータ
        /// </summary>
        [field: SerializeField]
        public OffensivePhysicalItemData[] ClawData { get; private set; }

        /// <summary>
        /// 牙のデータ
        /// </summary>
        [field: SerializeField]
        public OffensivePhysicalItemData[] FangData { get; private set; }

        /// <summary>
        /// 尻尾のデータ
        /// </summary>
        [field: SerializeField]
        public OffensivePhysicalItemData[] TailData { get; private set; }
    }
}
