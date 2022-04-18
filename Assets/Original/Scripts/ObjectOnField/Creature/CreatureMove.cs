using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物の移動
    /// </summary>
    public abstract class CreatureMove : CreatureSubComponent
    {
        /// <summary>
        /// 移動にかかる時間
        /// </summary>
        static readonly float MoveDuration = 0.25f;

        /// <summary>
        /// 回転にかかる時間
        /// </summary>
        static readonly float RotationDuration = 0.1f;

        /// <summary>
        /// 移動先の方向
        /// </summary>
        public Vector3 DirectionToTheNextPosition { get; protected set; } = Vector3.zero;

        /// <summary>
        /// 移動先の方向を設定する
        /// </summary>
        /// <param name="newDirection">方向</param>
        public void SetDirectionToTheNextPosition(Vector3 newDirection)
        {
            DirectionToTheNextPosition = newDirection;
        }

        /// <summary>
        /// 移動先の方向を設定する
        /// </summary>
        /// <param name="newDirection">方向</param>
        public void SetDirectionToTheNextPosition(Vector2 newDirection)
        {
            Vector3 newDirectionV3 = VectorUtility.ToVector3(newDirection);
            SetDirectionToTheNextPosition(newDirectionV3);
        }

        /// <summary>
        /// 回転を開始する
        /// </summary>
        public void StartRotation()
        {
            StartRotation(DirectionToTheNextPosition);
        }

        /// <summary>
        /// 回転を開始する
        /// </summary>
        /// <param name="direction">向く方向</param>
        public void StartRotation(Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                return;
            }

            Vector3 angle = VectorUtility.GetAngle(direction);
            Owner.SelfTransform.DOLocalRotate(angle, RotationDuration);
        }


        /// <summary>
        /// 移動を開始する
        /// </summary>
        public void StartMove()
        {
            StartMove(DirectionToTheNextPosition);
        }

        /// <summary>
        /// 移動を開始する
        /// </summary>
        /// <param name="direction">進む方向</param>
        public virtual void StartMove(Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                return;
            }

            Owner.SelfTransform.DOLocalMove(direction, MoveDuration)
                .SetRelative()
                .SetEase(Ease.Linear)
                .OnComplete(() => FinishMove());

            Owner.ChangeState(Creature.State.Move);
        }

        /// <summary>
        /// 移動を終了する
        /// </summary>
        protected void FinishMove()
        {
            Owner.ChangeState(Creature.State.Idle);
        }

        /// <summary>
        /// 指定された座標へ瞬間移動する
        /// </summary>
        /// <param name="newPosition">移動先</param>
        public void Teleport(Vector2Int newPosition)
        {

            Owner.SelfTransform.localPosition = VectorUtility.ToVector3(newPosition);
        }

        /// <summary>
        /// 指定された座標へ瞬間移動し、体の向きを変更する
        /// </summary>
        /// <param name="newPosition">移動先</param>
        /// <param name="newDirection">体の向き</param>
        public void Teleport(Vector2Int newPosition, Creature.Direction newDirection)
        {
            Teleport(newPosition);

            Vector3 angle = VectorUtility.GetAngle(newDirection);
            Owner.SelfTransform.DOLocalRotate(angle, 0.0f);
        }

        /// <summary>
        /// 移動を継続しているか
        /// </summary>
        /// <returns></returns>
        public bool IsMoving()
        {
            return ManagersMaster.Instance.InputM.AxisIsInput();
        }

        /// <summary>
        /// 移動方向から移動先の座標を求める
        /// </summary>
        /// <param name="direction">方向</param>
        /// <returns>座標</returns>
        public Vector2Int ToNewPosition(Vector3 direction)
        {
            return VectorUtility.ToVector2Int(Owner.SelfTransform.localPosition + direction);
        }

        /// <summary>
        /// 指定された座標のセルに入ることができるか
        /// </summary>
        /// <param name="position">座標</param>
        /// <returns></returns>
        public bool IsTravelableCell(Vector2Int position)
        {
            bool result = false;

            // セルがnullではない？
            Cell cell = ManagersMaster.Instance.FieldM.CurrentField.Data.GetCell(position);
            if (cell != null)
            {
                Cell.UnderType underType = cell.MyUnderType;
                Cell.MiddleType middleType = cell.MyMiddleType;
                Cell.UpperType upperType = cell.MyUpperType;
                Cell.UnderType[] intrudablesUnder = Owner.GetData().IntrudablesUnderTypes;
                Cell.MiddleType[] intrudablesMiddle = Owner.GetData().IntrudablesMiddleTypes;
                Cell.UpperType[] intrudablesUpper = Owner.GetData().IntrudablesUpperTypes;

                // 通行可能な下層または中層タイプと一致するものがある
                if (ContainsUnderLayerType(underType, intrudablesUnder) ||
                    ContainsMiddleLayerType(middleType, intrudablesMiddle))
                {
                    // 通行可能な上層タイプと一致するものがある
                    if (ContainsUpperLayerType(upperType, intrudablesUpper))
                    {
                        // 入れる
                        result = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 対象の上層レイヤータイプが、自身の通行可能な上層レイヤータイプの中に含まれているか
        /// </summary>
        /// <param name="type">対象のタイプ</param>
        /// <param name="intrudablesTypes">通行可能な上層レイヤータイプ</param>
        /// <returns></returns>
        public bool ContainsUpperLayerType(Cell.UpperType type, Cell.UpperType[] intrudablesTypes)
        {
            bool result = false;

            foreach (var val in intrudablesTypes)
            {
                if (val == type)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 対象の中層レイヤータイプが、自身の通行可能な中層レイヤータイプの中に含まれているか
        /// </summary>
        /// <param name="type">対象のタイプ</param>
        /// <param name="intrudablesTypes">通行可能な上層レイヤータイプ</param>
        /// <returns></returns>
        public bool ContainsMiddleLayerType(Cell.MiddleType type, Cell.MiddleType[] intrudablesTypes)
        {
            bool result = false;

            foreach (var val in intrudablesTypes)
            {
                if (val == type)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 対象の下層レイヤータイプが、自身の通行不可能な下層レイヤータイプの中に含まれているか
        /// </summary>
        /// <param name="type">対象のタイプ</param>
        /// <param name="notIntrudablesTypes">通行不可能な上層レイヤータイプ</param>
        /// <returns></returns>
        public bool ContainsUnderLayerType(Cell.UnderType type, Cell.UnderType[] notIntrudablesTypes)
        {
            bool result = false;

            foreach (var val in notIntrudablesTypes)
            {
                if (val == type)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 指定した座標に自分以外のフィールドオブジェクトが存在するか
        /// </summary>
        /// <param name="position">座標</param>
        /// <returns></returns>
        public bool CheckOtherObjects(Vector2Int position)
        {
            bool result = false;

            List<ObjectOnField> objects = ManagersMaster.Instance.ObjectsOnFieldM.ObjectsOnField;
            foreach (var val in objects)
            {
                Vector2Int objectsPosition = VectorUtility.ToVector2Int(val.SelfTransform.position);
                if (val != Owner && val.isActiveAndEnabled && position == objectsPosition)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 指定した座標に自分以外のフィールドオブジェクトの移動先が存在するか
        /// </summary>
        /// <returns></returns>
        public bool CheckOtherObjectsNextPosition(Vector2Int position)
        {
            bool result = false;

            List<ObjectOnField> objects = ManagersMaster.Instance.ObjectsOnFieldM.ObjectsOnField;
            foreach (var val in objects)
            {
                if (val != Owner && val.isActiveAndEnabled && position == val.NextPosition)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 指定した座標に敵対勢力が存在するか
        /// </summary>
        /// <param name="position">座標</param>
        /// <returns></returns>
        public abstract bool CheckOpponent(Vector2Int position);

        /// <summary>
        /// 指定した座標にプレゼント箱が存在するか
        /// </summary>
        /// <param name="position">座標</param>
        /// <returns></returns>
        public virtual bool CheckGiftBox(Vector2Int position)
        {
            return false;
        }

        /// <summary>
        /// 指定した座標にゴールが存在するか
        /// </summary>
        /// <param name="position">座標</param>
        /// <returns></returns>
        public virtual bool CheckGoal(Vector2Int position)
        {
            return false;
        }
    }
}
