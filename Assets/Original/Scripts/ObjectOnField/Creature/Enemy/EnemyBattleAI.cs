using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 敵の戦闘AI
    /// </summary>
    public class EnemyBattleAI : MonoBehaviour
    {
        /// <summary>
        /// タイプ
        /// </summary>
        public enum AIType
        {
            Random = 0, // ランダム
            DifferentPreviousChoice = 1, // 直前とは異なるアイテムを選ぶ
            Routine = 2 // 0番目から順番に選ぶ
        }

        /// <summary>
        /// 敵(戦闘)
        /// </summary>
        [field: SerializeField, HideInInspector]
        public EnemyBattle Owner { get; protected set; } = null;

        /// <summary>
        /// 直前に選択したアイテム番号
        /// </summary>
        public int? PreviousItemIndex { get; private set; } = null;

        /// <summary>
        /// 必要なコンポーネントをキャッシュする
        /// </summary>
        public void ChacheComponents()
        {
            Owner = GetComponent<EnemyBattle>();
        }

        /// <summary>
        /// 装備アイテムリストの内何番目のアイテムを使用するか選択する
        /// </summary>
        /// <returns></returns>
        public int? SelectEquippedItemIndex()
        {
            int? index = null;

            // 敵データ取得
            EnemyData enemyData = Owner.Owner.GetData() as EnemyData;

            AIType ai = enemyData.BattleAIType;

            switch (ai)
            {
                case AIType.Random:
                    index = GetRandomIndex();
                    break;

                case AIType.DifferentPreviousChoice:
                    index = GetRandomIndex(PreviousItemIndex);
                    break;

                case AIType.Routine:
                    index = GetNextIndex();
                    break;
            }

            PreviousItemIndex = index;
            return index;
        }

        /// <summary>
        /// いずれかの使用可能な番号を取得する
        /// </summary>
        /// <returns>使用可能なアイテムがなければnull</returns>
        private int? GetRandomIndex()
        {
            return GetRandomIndex(null);
        }

        /// <summary>
        /// いずれかの使用可能な番号を取得する
        /// </summary>
        /// <param name="disableIndex">使用不可番号</param>
        /// <returns>使用可能なアイテムがなければnull</returns>
        private int? GetRandomIndex(int? disableIndex)
        {
            // 使用するアイテムの番号
            int? index = null;

            // 装備アイテムリスト取得
            List<BattleItem> equippedItems = Owner.EquippedItems;

            // いずれかのアイテムを選択する
            int[] array = RandomUtility.GetRandomArray(Owner.EquippedItems.Count);

            foreach (var val in array)
            {
                if (equippedItems[val].CanUse() &&
                    val != disableIndex)
                {
                    index = val;
                    break;
                }
            }

            return index;
        }

        /// <summary>
        /// 直前に使用した番号の次の番号を取得する
        /// </summary>
        /// <returns>使用可能なアイテムがなければnull</returns>
        private int? GetNextIndex()
        {
            int? index = null;

            if (PreviousItemIndex.HasValue)
            {
                index = PreviousItemIndex + 1;
                if (index >= Owner.EquippedItems.Count)
                {
                    index = 0;
                }
                
            }
            else if (Owner.EquippedItems.Count > 0)
            {
                index = 0;
            }

            return index;
        }
    }
}
