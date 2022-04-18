using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    public class EnemyMove : CreatureMove
    {
        /// <summary>
        /// 敵の移動
        /// </summary>
        [field: SerializeField, HideInInspector]
        public EnemyMoveAI AI { get; protected set; } = null;

        public override void ChacheComponents()
        {
            base.ChacheComponents();

            AI = GetComponent<EnemyMoveAI>();

            AI.ChacheComponents();
        }

        public override bool CheckOpponent(Vector2Int position)
        {
            bool result = false;

            // 指定した座標がプレイヤーの移動先と同じか
            Player player = ManagersMaster.Instance.ObjectsOnFieldM.Player;
            if (position == player.NextPosition)
            {
                result = true;
            }

            return result;
        }
    }
}