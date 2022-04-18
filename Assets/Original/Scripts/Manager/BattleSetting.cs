using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 戦闘の設定
    /// </summary>
    [CreateAssetMenu(menuName = "Settings/BattleSetting")]
    public class BattleSetting : ScriptableObject
    {
        /// <summary>
        /// 弱点を突いたときのダメージ倍率
        /// </summary>
        [field: SerializeField]
        public float WeaknessAttributeMultiplier { get; private set; } = 1.5f;
    }

}

