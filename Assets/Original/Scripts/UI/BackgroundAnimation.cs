using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 背景テクスチャーのアニメーション
    /// </summary>
    public class BackgroundAnimation : MonoBehaviour
    {
        /// <summary>
        /// スクロールする速度
        /// </summary>
        [field: SerializeField]
        public Vector2 ScrollVelocity { get; private set; } = Vector2.zero;

        /// <summary>
        /// メッシュレンダラー
        /// </summary>
        [field: SerializeField]
        public MeshRenderer MeshRenderer { get; private set; } = new MeshRenderer();

        private void Reset()
        {
            CacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        private void CacheComponents()
        {
            MeshRenderer = GetComponent<MeshRenderer>();
        }

        void Update()
        {
            MoveTextureOffset();
        }

        /// <summary>
        /// テクスチャ－のオフセットを動かす
        /// </summary>
        void MoveTextureOffset()
        {
            // テクスチャの移動先を計算する
            // Time.time == ゲーム開始からの時間(秒)
            // Mathf.Repeat 第1引数が0～第2引数の間でループする 剰余演算子に似ている
            float x = Mathf.Repeat(ScrollVelocity.x * Time.time, 1.0f);
            float y = Mathf.Repeat(ScrollVelocity.y * Time.time, 1.0f);

            Vector2 offset = new Vector2(x, y);

            // テクスチャーのオフセットを変更する
            MeshRenderer.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
