using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 敵の戦闘
    /// </summary>
    public class EnemyBattle : CreatureBattle
    {
        /// <summary>
        /// 戦闘AI
        /// </summary>
        [field: SerializeField, HideInInspector]
        public EnemyBattleAI AI { get; protected set; } = null;

        public override void ChacheComponents()
        {
            base.ChacheComponents();

            AI = GetComponent<EnemyBattleAI>();

            AI.ChacheComponents();
        }
    }
}
