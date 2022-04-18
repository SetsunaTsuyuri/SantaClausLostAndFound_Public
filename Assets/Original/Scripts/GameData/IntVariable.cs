using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// int型のゲーム変数
    /// </summary>
    public class IntVariable : ScriptableObject
    {
        /// <summary>
        /// 値
        /// </summary>
        [field: SerializeField]
        public int Value { get; protected set; } = 0;

        /// <summary>
        /// 代入する
        /// </summary>
        /// <param name="newValue"></param>
        public virtual void Substitute(int newValue)
        {
            Value = newValue;
        }

        /// <summary>
        /// 足す
        /// </summary>
        /// <param name="addend">加数</param>
        public virtual void Add(int addend)
        {
            Substitute(Value + addend);
        }

        /// <summary>
        /// 引く
        /// </summary>
        /// <param name="subtrahend">減数</param>
        public virtual void Subtract(int subtrahend)
        {
            Substitute(Value - subtrahend);
        }
    }
}
