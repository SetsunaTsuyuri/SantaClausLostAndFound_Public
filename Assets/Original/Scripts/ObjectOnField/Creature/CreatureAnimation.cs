using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物のアニメーション
    /// </summary>
    public class CreatureAnimation : MonoBehaviour
    {
        /// <summary>
        /// アニメーターのリクエストIDハッシュ
        /// </summary>
        public static readonly int RequestIDHash = Animator.StringToHash("RequestID");

        public static readonly int Idle = Animator.StringToHash("Idle");

        public static readonly int Move = Animator.StringToHash("Move");

        public static readonly int Damaged = Animator.StringToHash("Damaged");

        public static readonly int Dead = Animator.StringToHash("Dead");

        /// <summary>
        /// アニメーションのID
        /// </summary>
        public enum ID
        {
            Idle = 0,
            Move = 1,
            Damaged = 2,
            Dead = 3
        }

        /// <summary>
        /// アニメーター
        /// </summary>
        [field: SerializeField, HideInInspector]
        public Animator SelfAnimator { get; protected set; } = null;

        private void Reset()
        {
            ChacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        protected virtual void ChacheComponents()
        {
            SelfAnimator = SelfAnimator = GetComponent<Animator>();
        }

        /// <summary>
        /// アニメーションリクエストIDを設定する
        /// </summary>
        /// <param name="id">アニメーションのID</param>
        public virtual void Play(ID id)
        {
            if (!SelfAnimator)
            {
                return;
            }

            SelfAnimator.SetInteger(RequestIDHash, (int)id);
        }

        /// <summary>
        /// 「待機」アニメーション開始時の処理
        /// </summary>
        public void OnStartIdle()
        {
            // リクエストIDを「待機」にする
            Play(ID.Idle);
        }
    }
}
