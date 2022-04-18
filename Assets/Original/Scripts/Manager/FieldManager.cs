using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// フィールドの管理者
    /// </summary>
    public class FieldManager : ManagerBase
    {
        /// <summary>
        /// セレクトシーンで選択可能なステージデータの配列
        /// </summary>
        [field: SerializeField]
        public StageData[] StageDataArray { get; private set; } = { };

        [field: SerializeField]
        public Field FieldPrefab { get; private set; } = null;

        [field: SerializeField]
        public Transform FieldParent { get; private set; } = null;

        /// <summary>
        /// 現在のフィールド
        /// </summary>
        public Field CurrentField { get; private set; } = null;

        /// <summary>
        /// 初期化する
        /// </summary>
        public override void Init()
        {
            base.Init();

            if (!FieldPrefab)
            {
                StageData stageData = GameController.Instance.GetSelectedStageData();

                int index = GameController.Instance.CurrentFieldLevel;
                FieldPrefab = stageData.Fields[index];
            }

            CurrentField = Instantiate(FieldPrefab, FieldParent);
            CurrentField.Init();
        }
    }
}
