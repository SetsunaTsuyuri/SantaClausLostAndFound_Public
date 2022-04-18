using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// フィールドを構成するマス目
    /// </summary>
    [System.Serializable]
    public class Cell
    {
        /// <summary>
        /// 下層のタイプ
        /// </summary>
        public enum UnderType
        {
            None = 0,
            Floor = 1,
            Water = 2,
            Wall = 3
        }

        /// <summary>
        /// 中層のタイプ
        /// </summary>
        public enum MiddleType
        {
            None = 0,
            Bridge = 11,
            SecretPassage = 12
        }

        /// <summary>
        /// 上層のタイプ
        /// </summary>
        public enum UpperType
        {
            None = 0,
            Grass = 21,
            Tree = 22
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="under">下層レイヤー</param>
        /// <param name="upper">上層レイヤー</param>
        public Cell(UnderType under, MiddleType middle ,UpperType upper)
        {
            MyUnderType = under;
            MyMiddleType = middle;
            MyUpperType = upper;
        }

        /// <summary>
        /// セルの下層タイプ
        /// </summary>
        [field: SerializeField]
        public UnderType MyUnderType { get; private set; } = UnderType.None;

        /// <summary>
        /// セルの中層タイプ
        /// </summary>
        [field: SerializeField]
        public MiddleType MyMiddleType { get; private set; } = MiddleType.None;

        /// <summary>
        /// セルの上層タイプ
        /// </summary>
        [field: SerializeField]
        public UpperType MyUpperType { get; private set; } = UpperType.None;
    }
}
