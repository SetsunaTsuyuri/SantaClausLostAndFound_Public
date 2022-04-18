using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 特殊行動のデータリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Action Data/Special Action Data List")]
    public class SpecialActionDataList : ScriptableObject
    {
        /// <summary>
        /// 配列何番目のデータを読み込むか
        /// </summary>
        public enum ID
        {
            NoAction = 0
        }

        /// <summary>
        /// 特殊アクションデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] Data { get; private set; } = null;
    }

}
