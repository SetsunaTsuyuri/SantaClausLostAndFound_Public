using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// アイテム
    /// </summary>
    public abstract class Item : MonoBehaviour
    {
        /// <summary>
        /// 配列何番目のデータを読み込むか
        /// </summary>
        [field: SerializeField]
        public int ID { get; protected set; } = 0;

        /// <summary>
        /// 自身のデータを取得する
        /// </summary>
        /// <returns>アイテムのデータ</returns>
        public abstract ItemData GetData();
    }
}
