using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 攻撃系物理アクションデータリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Action Data/Offensive Physical Action Data List")]
    public class OffensivePhysicalActionDataList : ScriptableObject
    {
        /// <summary>
        /// 配列何番目のデータを読み込むか
        /// </summary>
        public enum ID
        {
            NormalAttack = 0
        }

        /// <summary>
        /// 剣アクションのデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] SwordActionData { get; private set; }

        /// <summary>
        /// 槍アクションのデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] SpearActionData { get; private set; }

        /// <summary>
        /// 槌アクションのデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] HammerActionData { get; private set; }

        /// <summary>
        /// 爪アクションのデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] ClawActionData { get; private set; }

        /// <summary>
        /// 牙アクションのデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] FangActionData { get; private set; }

        /// <summary>
        /// 尻尾アクションのデータ
        /// </summary>
        [field: SerializeField]
        public ActionData[] TailActionData { get; private set; }
    }
}
