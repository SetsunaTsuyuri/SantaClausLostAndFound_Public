using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 敵のデータ
    /// </summary>
    [System.Serializable]
    public class EnemyData : CreatureData
    {
        /// <summary>
        /// 経験値
        /// </summary>
        [field: SerializeField]
        public int Experience { get; private set; } = 100;

        /// <summary>
        /// 戦闘AIタイプ
        /// </summary>
        [field: SerializeField]
        public EnemyBattleAI.AIType BattleAIType { get; private set; } = EnemyBattleAI.AIType.Random;

        /// <summary>
        /// カメラターゲット(Aim)のY座標オフセット
        /// </summary>
        [field: SerializeField]
        public float AimedTargetOffsetY { get; private set; } = 0.0f;

        /// <summary>
        /// カメラターゲット(Follow)のZ座標オフセット
        /// </summary>
        [field: SerializeField]
        public float FollowedTargetOffsetZ { get; private set; } = 0.0f;

        [field: SerializeField]
        public bool IsBoss { get; private set; } = false;
    }
}
