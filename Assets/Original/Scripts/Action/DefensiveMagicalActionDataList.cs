using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 魔法防御アクションデータの配列
    /// </summary>
    [CreateAssetMenu(menuName = "Action Data/Defensive Magical Action Data List")]
    public class DefensiveMagicalActionDataList : ScriptableObject
    {
        /// <summary>
        /// 配列何番目のデータを読み込むか
        /// </summary>
        public enum ID
        {
            Heal = 0
        }

        /// <summary>
        /// 魔法の杖アクションのデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] StaffActionData { get; private set; }
    }
}
