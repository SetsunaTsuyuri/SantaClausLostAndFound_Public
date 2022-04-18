using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレイヤーのデータ
    /// </summary>
    [System.Serializable]
    public class PlayerData : CreatureData
    {
        /// <summary>
        /// HP回復に必要な歩数
        /// </summary>
        [field: SerializeField]
        public int StepsRequiredToRecoverHp { get; private set; } = 2;
    }
}

