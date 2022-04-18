using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 魔法防御アイテムのデータ
    /// </summary>
    [System.Serializable]
    public class DefensiveMagicalItemData : MagicalItemData
    {
        /// <summary>
        /// 実行する行動のID
        /// </summary>
        [field: SerializeField]
        public DefensiveMagicalActionDataList.ID ActionID { get; private set; } = 0;
    }
}
