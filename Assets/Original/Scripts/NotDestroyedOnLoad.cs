using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// シーンを跨いで存在し続ける
    /// </summary>
    public class NotDestroyedOnLoad : SingletonMonoBehaviour<NotDestroyedOnLoad>
    {
        protected override void AwakeInner()
        {
            base.AwakeInner();

            Init();
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        private void Init()
        {
            if (Instance != null)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.Log("ぬるじゃああ");
            }
        }
    }
}
