using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// フィールド上の物体
    /// </summary>
    public abstract class ObjectOnField : MonoBehaviour
    {
        /// <summary>
        /// 移動先の座標
        /// </summary>
        public Vector2Int NextPosition { get; protected set; } = Vector2Int.zero;

        /// <summary>
        /// トランスフォーム
        /// </summary>
        [field: SerializeField, HideInInspector]
        public Transform SelfTransform { get; protected set; } = null;

        private void Reset()
        {
            ChacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        protected virtual void ChacheComponents()
        {
            SelfTransform = transform;
        }

        public virtual void Init()
        {
            ResetNextPosition();
        }

        /// <summary>
        /// 移動先の座標を決定する
        /// </summary>
        /// <param name="newPosition"></param>
        public void SetNextPosition(Vector2Int newPosition)
        {
            NextPosition = newPosition;
        }

        /// <summary>
        /// 移動先の座標を現在地に戻す
        /// </summary>
        public void ResetNextPosition()
        {
            Vector3 position = SelfTransform.position;
            NextPosition = VectorUtility.ToVector2Int(position);
        }

        /// <summary>
        /// プレイヤーに触れられた場合の処理
        /// </summary>
        public virtual void OnTouchedByPlayer() { }
    }
}
