using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    public class PlayerMove : CreatureMove
    {
        /// <summary>
        /// 現在の歩数
        /// </summary>
        public int CurrentSteps { get; private set; } = 0; 

        public override bool CheckOpponent(Vector2Int position)
        {
            bool result = false;

            // 移動先がいずれかの敵の位置と同じか
            Enemy[] enemies = ManagersMaster.Instance.ObjectsOnFieldM.Enemies;
            foreach (var enemy in enemies)
            {
                if (enemy.CurrentState != Creature.State.Dead)
                {
                    Vector2Int enemyPosition = VectorUtility.ToVector2Int(enemy.SelfTransform.position);
                    if (position == enemyPosition)
                    {
                        result = true;
                        enemy.OnTouchedByPlayer();
                    }
                }
            }

            return result;
        }

        public override void StartMove(Vector3 direction)
        {
            base.StartMove(direction);

            AddStepsAndRecoverHp();            
        }

        /// <summary>
        /// 歩数増加とHPの回復を行う
        /// </summary>
        private void AddStepsAndRecoverHp()
        {
            CurrentSteps++;

            PlayerData playerData = Owner.GetData() as PlayerData;
            if (CurrentSteps % playerData.StepsRequiredToRecoverHp == 0)
            {
                Owner.SelfBattle.RecoverHp(1);
            }
        }

        public override bool CheckGiftBox(Vector2Int position)
        {
            GiftBox[] giftBoxes = ManagersMaster.Instance.ObjectsOnFieldM.GiftBoxes;
            if (giftBoxes == null)
            {
                return false;
            }

            bool result = false;

            foreach (var box in giftBoxes)
            {
                if (box.CurrentState == GiftBox.State.Uncollected)
                {
                    Vector2Int boxPosition = VectorUtility.ToVector2Int(box.SelfTransform.position);
                    if (boxPosition == position)
                    {
                        result = true;
                        box.OnTouchedByPlayer();
                    }
                }
            }

            return result;
        }

        public override bool CheckGoal(Vector2Int position)
        {
            Goal goal = ManagersMaster.Instance.ObjectsOnFieldM.Goal;
            if (goal == null)
            {
                return false;
            }

            bool result = false;
            Vector2Int goalPosition = VectorUtility.ToVector2Int(goal.SelfTransform.position);
            if (goalPosition == position)
            {
                result = true;
            }

            return result;
        }
    }
}
