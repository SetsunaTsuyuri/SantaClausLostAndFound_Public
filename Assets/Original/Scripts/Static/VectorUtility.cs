using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// ベクトル関連の便利クラス
    /// </summary>
    public class VectorUtility
    {
        /// <summary>
        /// Vector3をVector2Intに変換する (Xint = X, Yint = Z)
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector2Int ToVector2Int(Vector3 vector3)
        {
            int x = Mathf.FloorToInt(vector3.x);
            int y = Mathf.FloorToInt(vector3.z);

            return new Vector2Int(x, y);
        }

        /// <summary>
        /// Vector2intをVector3に変換する
        /// </summary>
        /// <param name="vector2Int"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(Vector2Int vector2Int)
        {
            float x = vector2Int.x;
            float y = 0.0f;
            float z = vector2Int.y;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Vector2をVector3に変換する
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(Vector2 vector2)
        {
            float x = vector2.x;
            float y = 0.0f;
            float z = vector2.y;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Vector3から角度を求める
        /// </summary>
        /// <param name="direction">向く方向</param>
        /// <returns></returns>
        public static Vector3 GetAngle(Vector3 direction)
        {
            Vector3 angle = Quaternion.LookRotation(direction).eulerAngles;
            return angle;
        }

        /// <summary>
        /// 方向から角度を求める
        /// </summary>
        /// <param name="direction">向く方向</param>
        /// <returns></returns>
        public static Vector3 GetAngle(Creature.Direction direction)
        {
            Vector3 angle = Vector3.zero;

            switch (direction)
            {
                case Creature.Direction.North:
                    angle.y = (float)Creature.Direction.North;
                    break;

                case Creature.Direction.East:
                    angle.y = (float)Creature.Direction.East;
                    break;

                case Creature.Direction.West:
                    angle.y = (float)Creature.Direction.West;
                    break;

                case Creature.Direction.South:
                    angle.y = (float)Creature.Direction.South;
                    break;
            }

            return angle;
        }
    }
}
