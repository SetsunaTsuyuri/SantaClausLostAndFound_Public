using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 浮かぶ表示の設定
    /// </summary>
    [CreateAssetMenu(menuName = "Settings/FloatDisplaySetting")]
    public class FloatDisplaySetting : ScriptableObject
    {
        /// <summary>
        /// 移動先の相対座標
        /// </summary>
        [field: SerializeField]
        public Vector2 Position { get; private set; } = new Vector2(0.0f, 50.0f);

        /// <summary>
        /// 移動時間
        /// </summary>
        [field: SerializeField]
        public float MoveDuration { get; private set; } = 0.5f;

        /// <summary>
        /// 静止時間
        /// </summary>
        [field: SerializeField]
        public float StationaryDuration { get; private set; } = 0.5f;

        /// <summary>
        /// フェード時間
        /// </summary>
        [field: SerializeField]
        public float FadeDuration { get; private set; } = 0.5f;
    }
}
