using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレイヤーに関する設定
    /// </summary>
    [CreateAssetMenu(menuName = "Settings/PlayerSetting")]
    public class PlayerSetting : ScriptableObject
    {
        /// <summary>
        /// レベルアップに必要な経験値の基本値
        /// </summary>
        [field: SerializeField]
        public int ExperienceRequiredToLevelUp { get; private set; } = 100;

        /// <summary>
        /// レベルアップに必要な経験値の係数
        /// </summary>
        [field: SerializeField]
        public float ExperienceCoefficient { get; private set; } = 1.1f;
    }
}
