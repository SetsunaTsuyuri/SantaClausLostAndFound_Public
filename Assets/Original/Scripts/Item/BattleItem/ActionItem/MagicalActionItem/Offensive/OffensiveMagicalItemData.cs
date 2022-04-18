using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 魔法攻撃系アイテムのデータ
    /// </summary>
    [System.Serializable]
    public class OffensiveMagicalItemData : MagicalItemData
    {
        /// <summary>
        /// 実行する行動のID
        /// </summary>
        [field: SerializeField]
        public OffensiveMagicalActionDataList.ID ActionID { get; private set; } = 0;
    }
}
