using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 物理攻撃アイテムのデータ
    /// </summary>
    [System.Serializable]
    public class OffensivePhysicalItemData : PhysicalItemData
    {
        /// <summary>
        /// 実行する行動のID
        /// </summary>
        [field: SerializeField]
        public OffensivePhysicalActionDataList.ID ActionID { get; private set; }
    }
}
