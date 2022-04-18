using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DungeonRPG3D
{
    /// <summary>
    /// 移動先表示パネル
    /// </summary>
    public class NextPositionPanel : MonoBehaviour
    {
        /// <summary>
        /// フェード時間
        /// </summary>
        public static readonly float Duration = 0.5f;

        /// <summary>
        /// トランスフォーム
        /// </summary>
        [field: SerializeField]
        public Transform SelfTransform { get; private set; } = null;

        /// <summary>
        /// レンダラー
        /// </summary>
        [field: SerializeField]
        public Renderer SelfRenderer { get; private set; } = null;

        /// <summary>
        /// 誰の移動先を示すか
        /// </summary>
        public Creature Target { get; private set; } = null;

        /// <summary>
        /// フェード処理Tweener
        /// </summary>
        public Tweener FadeLoopTweener { get; private set; } = null;

        private void Reset()
        {
            ChacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        private void ChacheComponents()
        {
            SelfTransform = transform;
            SelfRenderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        public void Init(Creature creature)
        {
            Target = creature;
            FadeLoop();
        }

        /// <summary>
        /// フェード処理を繰り返す
        /// </summary>
        public void FadeLoop()
        {
            FadeLoopTweener = SelfRenderer.material.DOFade(0.0f, Duration)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void Update()
        {
            UpdateTweener();

            if (!FadeLoopTweener.IsActive())
            {
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Tweenerの更新処理
        /// </summary>
        private void UpdateTweener()
        {
            // Tweenがアクティブの場合
            if (FadeLoopTweener.IsActive())
            {
                // 対象が死亡している場合
                if (Target.CurrentState == Creature.State.Dead)
                {
                    // Tweenを殺す
                    FadeLoopTweener.Kill();

                }
            }
        }
    }
}
