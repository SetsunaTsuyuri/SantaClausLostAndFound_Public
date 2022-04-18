using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物のデータ
    /// </summary>
    public class CreatureData
    {
        /// <summary>
        /// 名前
        /// </summary>
        [field: SerializeField]
        public string Name { get; private set; } = "NO NAME";

        /// <summary>
        /// モデル
        /// </summary>
        [field: SerializeField]
        public GameObject Model { get; private set; } = null;

        /// <summary>
        /// 侵入可能なセルの下層タイプ
        /// </summary>
        [field: SerializeField]
        public Cell.UnderType[] IntrudablesUnderTypes { get; private set; } = { Cell.UnderType.Floor };

        /// <summary>
        /// 侵入可能なセルの中層タイプ
        /// </summary>
        [field: SerializeField]
        public Cell.MiddleType[] IntrudablesMiddleTypes { get; private set; } = { Cell.MiddleType.Bridge };

        /// <summary>
        /// 侵入可能なセルの上層タイプ
        /// </summary>
        [field: SerializeField]
        public Cell.UpperType[] IntrudablesUpperTypes { get; set; } = { Cell.UpperType.None };

        /// <summary>
        /// 装備アイテム
        /// </summary>
        [field: SerializeField]
        public BattleItem[] EquippedItems { get; private set; } = { };

        /// <summary>
        /// ステータス
        /// </summary>
        [field: SerializeField]
        public CreatureStatus Status { get; private set; } = new CreatureStatus();

        /// <summary>
        /// 弱点属性
        /// </summary>
        [field: SerializeField]
        public ActionData.AttributeType[] WeaknessAttributes { get; private set; } = { };
    }
}
