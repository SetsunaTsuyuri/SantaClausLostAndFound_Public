using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// フィールドオブジェクトの生成装置
    /// </summary>
    public class ObjectOnFieldGenerator : MonoBehaviour
    {
        /// <summary>
        /// 自分自身のトランスフォーム
        /// </summary>
        [field: SerializeField, HideInInspector]
        public Transform SelfTransform { get; private set; } = null;

        /// <summary>
        /// 生成するフィールドオブジェクトプレファブ
        /// </summary>
        [field: SerializeField]
        public ObjectOnField ObjectPrefab { get; private set; } = null;

        /// <summary>
        /// 初期位置
        /// </summary>
        [field: SerializeField]
        public Vector2Int ObjectPositon { get; private set; } = Vector2Int.zero;

        /// <summary>
        /// 初期方向
        /// </summary>
        [field: SerializeField]
        public Creature.Direction ObjectDirection { get; private set; } = Creature.Direction.North;

        /// <summary>
        /// 生成したフィールドオブジェクトインスタンス
        /// </summary>
        public ObjectOnField ObjectInstance { get; private set; } = null;

        private void Reset()
        {
            ChacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        public void ChacheComponents()
        {
            SelfTransform = transform;
        }

        /// <summary>
        /// オブジェクトを生成する
        /// </summary>
        public virtual void Generate()
        {
            if (!ObjectPrefab)
            {
                return;
            }
            
            ObjectInstance = Instantiate(ObjectPrefab, SelfTransform);
            ObjectInstance.SelfTransform.position = VectorUtility.ToVector3(ObjectPositon);
            ObjectInstance.SelfTransform.rotation = Quaternion.Euler(VectorUtility.GetAngle(ObjectDirection));
        }
    }
}
