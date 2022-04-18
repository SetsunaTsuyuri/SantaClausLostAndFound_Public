using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 物理防御アイテムのデータ
    /// </summary>
    [System.Serializable]
    public class DefensivePhysicalItemData : PhysicalItemData
    {
        /// <summary>
        /// 実行する行動のID
        /// </summary>
        [field: SerializeField]
        public DefensivePhysicalActionDataList.ID ActionID { get; private set; }
    }
}
