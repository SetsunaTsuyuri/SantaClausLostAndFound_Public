using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 敵の生成装置
    /// </summary>
    public class EnemyGenerator : ObjectOnFieldGenerator
    {
        /// <summary>
        /// 敵のID
        /// </summary>
        [field: SerializeField]
        public int ID { get; private set; } = 1;

        /// <summary>
        /// 敵のレベル
        /// </summary>
        [field: SerializeField]
        public int Level { get; private set; } = 1;

        /// <summary>
        /// 敵の移動AIタイプ
        /// </summary>
        [field: SerializeField]
        public EnemyMoveAI.AIType MoveAI { get; private set; } = EnemyMoveAI.AIType.NoMove;

        public override void Generate()
        {
            base.Generate();

            Enemy enemy = ObjectInstance as Enemy;
            enemy.SetID(ID);
            enemy.SelfBattle.SetLevel(Level);

            EnemyMoveAI enemyMoveAI = (enemy.SelfMove as EnemyMove).AI;
            enemyMoveAI.SetAIType(MoveAI);
        }
    }
}
