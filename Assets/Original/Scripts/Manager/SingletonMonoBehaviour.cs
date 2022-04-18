using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// シングルトンパターンのMonoBehaviour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        public static T Instance { get; private set; } = null;

        private void Awake()
        {
            AwakeInner();
        }

        /// <summary>
        /// 最初に実行する処理
        /// </summary>
        protected virtual void AwakeInner()
        {
            SetInstance();
        }

        /// <summary>
        /// このクラスのインスタンスを設定する
        /// </summary>
        private void SetInstance()
        {
            if (Instance == null)
            {
                Instance = (T)FindObjectOfType(typeof(T));
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
