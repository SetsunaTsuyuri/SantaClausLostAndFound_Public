using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 敵の移動AI
    /// </summary>
    public class EnemyMoveAI : MonoBehaviour
    {
        /// <summary>
        /// タイプ
        /// </summary>
        public enum AIType
        {
            NoMove = 0, // 動かない
            StraightRandom = 1, // 直線移動のランダム
            DiagonalRandom, //斜め移動のランダム
            Advancing // 前進を続け、移動できなくなったら直線のランダム移動を試みる
        }

        /// <summary>
        /// 敵(移動)
        /// </summary>
        [field: SerializeField, HideInInspector]
        public EnemyMove Owner { get; private set; } = null;

        /// <summary>
        /// AIタイプ
        /// </summary>
        public AIType Type { get; private set; } = AIType.NoMove;

        /// <summary>
        /// 必要なコンポーネントをキャッシュする
        /// </summary>
        public void ChacheComponents()
        {
            Owner = GetComponent<EnemyMove>();
        }

        /// <summary>
        /// AIタイプを設定する
        /// </summary>
        /// <param name="newAIType">AIタイプ</param>
        public void SetAIType(AIType newAIType)
        {
            Type = newAIType;
        }

        /// <summary>
        /// 移動方向を選択する
        /// </summary>
        public Vector3 SelectMoveDirection()
        {
            // 移動方向
            Vector3 direction = Vector3.zero;

            switch (Type)
            {
                case AIType.StraightRandom:
                    direction = GetRandomDirection(ToStraightDirecotion);
                    break;

                case AIType.DiagonalRandom:
                    direction = GetRandomDirection(ToDiagonalDirecotion);
                    break;

                case AIType.Advancing:
                    direction = Advance();
                    break;
            }

            return direction;
        }

        /// <summary>
        /// 移動方向を求めるデリゲード
        /// </summary>
        /// <param name="directionType">移動方向</param>
        /// <returns>移動方向</returns>
        delegate Vector3 MoveDirection(int directionType);

        /// <summary>
        /// 直線の4方向を取得する
        /// </summary>
        /// <param name="directionType"></param>
        /// <returns></returns>
        private Vector3 ToStraightDirecotion(int directionType)
        {
            Vector3 direction = Vector3.zero;

            switch (directionType)
            {
                case 0:
                    direction = Vector3.forward;
                    break;

                case 1:
                    direction = Vector3.right;
                    break;

                case 2:
                    direction = Vector3.left;
                    break;

                case 3:
                    direction = Vector3.back;
                    break;
            }

            return direction;
        }

        /// <summary>
        /// 斜めの4方向を取得する
        /// </summary>
        /// <param name="directionType">移動方向</param>
        /// <returns>移動方向</returns>
        private Vector3 ToDiagonalDirecotion(int directionType)
        {
            Vector3 direction = Vector3.zero;

            switch (directionType)
            {
                case 0:
                    direction = new Vector3(1.0f, 0.0f, 1.0f);
                    break;

                case 1:
                    direction = new Vector3(1.0f, 0.0f, -1.0f);
                    break;

                case 2:
                    direction = new Vector3(-1.0f, 0.0f, 1.0f);
                    break;

                case 3:
                    direction = new Vector3(-1.0f, 0.0f, -1.0f);
                    break;
            }

            return direction;
        }


        /// <summary>
        /// いずれかの移動可能な方向を取得する
        /// </summary>
        /// <returns>どの方向にも移動不可能ならVector3.zero</returns>
        private Vector3 GetRandomDirection(MoveDirection moveDirection)
        {
            // 移動する方向
            Vector3 direction = Vector3.zero;

            // 4方向いずれかに進行可能か、または敵対勢力に接触できるかを試す
            int[] array = RandomUtility.GetRandomArray(4);
            foreach (var val in array)
            {
                // 仮の方向を決める
                Vector3 tempDirection = moveDirection(val);

                // 仮の方向に進行可能か、または敵対勢力がいる場合、その方向に決定する
                Vector2Int tempDestination = Owner.ToNewPosition(tempDirection);
                if (Owner.IsTravelableCell(tempDestination) &&
                    !Owner.CheckOtherObjectsNextPosition(tempDestination) ||
                    Owner.CheckOpponent(tempDestination))
                {
                    direction = tempDirection;
                    break;
                }
            }
            return direction;
        }

        /// <summary>
        /// 前進する(移動不可の場合直線ランダム移動)
        /// </summary>
        /// <returns>移動方向</returns>
        private Vector3 Advance()
        {
            Vector3 direction = Vector3.zero;

            // 仮の方向を決める
            Vector3 tempDirection = Owner.Owner.SelfTransform.forward;

            Vector2Int tempDestination = Owner.ToNewPosition(tempDirection);
            if (Owner.IsTravelableCell(tempDestination) &&
                !Owner.CheckOtherObjectsNextPosition(tempDestination) ||
                Owner.CheckOpponent(tempDestination))
            {
                direction = tempDirection;
            }
            else
            {
                direction = GetRandomDirection(ToStraightDirecotion);
            }

            return direction;
        }
    }
}
