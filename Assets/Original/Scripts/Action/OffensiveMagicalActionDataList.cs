using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 魔法攻撃アクションデータリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Action Data/Offensive Magical Action Data List")]
    public class OffensiveMagicalActionDataList : ScriptableObject
    {
        /// <summary>
        /// 配列何番目の行動を読み込むか
        /// </summary>
        public enum ID
        {
            Fire = 0,
            Ice = 1,
            Thunder = 2
        }

        /// <summary>
        /// 魔導書アクションのデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] GrimoireActionData { get; private set; }

        /// <summary>
        /// 竜石アクションのデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] DragonGemActionData { get; private set; }
    }
}
