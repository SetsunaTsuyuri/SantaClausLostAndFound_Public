using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレイヤーのデータを纏めた配列
    /// </summary>
    [CreateAssetMenu(menuName = "Creature Data/Player Data List")]
    public class PlayerDataList : ScriptableObject
    {
        /// <summary>
        /// プレイヤーデータの配列
        /// </summary>
        [field: SerializeField]
        public PlayerData[] Data { get; private set; }
    }
}

