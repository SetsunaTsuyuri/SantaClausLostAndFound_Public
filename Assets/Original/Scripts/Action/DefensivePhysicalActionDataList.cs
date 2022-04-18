using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 物理防御アクションデータリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Action Data/Defensive Physical Action Data List")]
    public class DefensivePhysicalActionDataList : ScriptableObject
    {
        /// <summary>
        /// 配列何番目のデータを読み込むか
        /// </summary>
        public enum ID
        {
            NormalGuard = 0,
            Heal = 1
        }

        /// <summary>
        /// 盾アクションのデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] ShieldActionData { get; private set; }
    }
}
