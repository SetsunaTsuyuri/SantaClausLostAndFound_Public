using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 行動データの管理者
    /// </summary>
    public class ActionDataManager : MonoBehaviour
    {
        /// <summary>
        /// 物理攻撃アクションのデータ
        /// </summary>
        [field: SerializeField]
        public OffensivePhysicalActionDataList OffensivePhysicalActionDataList { get; private set; } = null;

        /// <summary>
        /// 物理防御アクションのデータ
        /// </summary>
        [field: SerializeField]
        public DefensivePhysicalActionDataList DefensivePhysicalActionDataList { get; private set; } = null;

        /// <summary>
        /// 魔法攻撃アクションのデータ
        /// </summary>
        [field: SerializeField]
        public OffensiveMagicalActionDataList OffensiveMagicalActionDataList { get; private set; } = null;

        /// <summary>
        /// 魔法防御アクションのデータ
        /// </summary>
        [field: SerializeField]
        public DefensiveMagicalActionDataList DefensiveMagicalActionDataList { get; private set; } = null;

        /// <summary>
        /// 特殊アクションのデータ
        /// </summary>
        [field: SerializeField]
        public SpecialActionDataList SpecialActionDataList { get; private set; } = null;
    }
}
