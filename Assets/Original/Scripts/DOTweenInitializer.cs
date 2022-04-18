using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DungeonRPG3D
{
    /// <summary>
    /// DOTweenの初期化を行うクラス
    /// </summary>
    public class DOTweenInitializer : MonoBehaviour
    {
        private void Start()
        {
            InitDOTween();
        }

        /// <summary>
        /// DOTweenの初期化を行う
        /// </summary>
        private void InitDOTween()
        {
            DOTween.Init(true);
        }
    }
}
