using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DungeonRPG3D
{
    /// <summary>
    /// 行動のデータ
    /// </summary>
    [System.Serializable]
    public class ActionData
    {
        /// <summary>
        /// 対象の種類
        /// </summary>
        public enum TargetType
        {
            None = 0,     // 対象なし
            Opponent = 1, // 相手
            MySelf = 2,   // 自分
        }

        /// <summary>
        /// HPに対する影響の種類
        /// </summary>
        public enum ImpactOnHpType
        {
            None = 0,   // 影響なし
            Damage = 1, // ダメージ
            Heal = 2,   // 回復
        }

        /// <summary>
        /// 計算式の種類
        /// </summary>
        public enum CalculationType
        {
            None = 0,       // 計算しない
            TrueDamage = 1, // 固定
            Physical = 2,   // 物理
            Magical = 3,    // 魔法
        }

        /// <summary>
        /// 属性の種類
        /// </summary>
        public enum AttributeType
        {
            None = 0,     // 無 

            Slashing = 1, // 斬
            Stabbing = 2, // 突
            Blow = 3,     // 打

            Fire = 11,    // 炎
            Ice = 12,     // 氷
            Thunder = 13, // 雷
        }

        /// <summary>
        /// 名前
        /// </summary>
        [field: SerializeField]
        public string Name { get; private set; } = "No Name";

        /// <summary>
        /// 対象
        /// </summary>
        [field: SerializeField]
        public TargetType Target { get; private set; } = TargetType.None;

        /// <summary>
        /// HPに対する影響
        /// </summary>
        [field: SerializeField]
        public ImpactOnHpType ImpactOnHp { get; private set; } = ImpactOnHpType.Damage;

        /// <summary>
        /// 計算式
        /// </summary>
        [field: SerializeField]
        public CalculationType Calculation { get; private set; } = CalculationType.TrueDamage;

        /// <summary>
        /// 属性
        /// </summary>
        [field: SerializeField]
        public AttributeType Attribute { get; private set; } = AttributeType.None;

        /// <summary>
        /// 行動回数
        /// </summary>
        [field: SerializeField]
        public int NumberOfActions { get; private set; } = 1;

        /// <summary>
        /// 行動選択後、即座に実行されるか？
        /// </summary>
        [field: SerializeField]
        public bool IsQuick { get; private set; } = false;

        /// <summary>
        /// 威力
        /// </summary>
        [field: SerializeField]
        public int Power { get; private set; } = 0;
    }
}
