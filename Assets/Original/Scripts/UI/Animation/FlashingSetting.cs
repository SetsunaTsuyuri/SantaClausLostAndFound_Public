using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 点滅の設定
    /// </summary>
    [CreateAssetMenu(menuName = "Settings/FlashingSetting")]
    public class FlashingSetting : ScriptableObject
    {
        [field: SerializeField]
        public float FadeInDuration { get; private set; } = 0.1f;

        [field: SerializeField]
        public float FadeOutDuration { get; private set; } = 0.3f;

        [field: SerializeField]
        public float WaitingTime { get; private set; } = 0.3f;
    }

}
