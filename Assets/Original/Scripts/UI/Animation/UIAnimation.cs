using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DungeonRPG3D
{
    /// <summary>
    /// UIアニメーション
    /// </summary>
    public class UIAnimation : MonoBehaviour
    {
        /// <summary>
        /// フェードアウト
        /// </summary>
        public static readonly float FadeOut = 1.0f;

        /// <summary>
        /// フェードイン
        /// </summary>
        public static readonly float FadeIn = 0.0f;

        /// <summary>
        /// 透明
        /// </summary>
        public static readonly float Transparent = 0.0f;

        /// <summary>
        /// 非透明
        /// </summary>
        public static readonly float NotTransparent = 1.0f;

        /// <summary>
        /// 引数なしのときのイージング
        /// </summary>
        public static readonly Ease DefaultEase = Ease.OutQuad;

        /// <summary>
        /// レクトトランスフォーム
        /// </summary>
        [field: SerializeField]
        public RectTransform SelfTransform { get; private set; } = null;

        /// <summary>
        /// キャンバスグループ
        /// </summary>
        [field: SerializeField]
        public CanvasGroup SelfCanvasGroup { get; private set; } = null;

        /// <summary>
        /// 初期の座標
        /// </summary>
        public Vector2 InitialPosition { get; private set; } = Vector2.zero;

        /// <summary>
        /// 初期の不透明度
        /// </summary>
        public float InitialAlpha { get; private set; } = 0.0f;

        private void Reset()
        {
            ChacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        private void ChacheComponents()
        {
            SelfTransform = GetComponent<RectTransform>();
            SelfCanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Awake()
        {
            Init();
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        public void Init()
        {
            InitialPosition = SelfTransform.anchoredPosition;

            if (SelfCanvasGroup)
            {
                InitialAlpha = SelfCanvasGroup.alpha;
            }
        }

        /// <summary>
        /// 座標を初期化する
        /// </summary>
        public void InitPosition()
        {
            SelfTransform.anchoredPosition = InitialPosition;
        }

        /// <summary>
        /// 移動する
        /// </summary>
        /// <param name="newPosition">新しい位置</param>
        /// <param name="duration">時間</param>
        /// <param name="ease">イージング</param>
        /// <param name="isLoop">ループするか</param>
        public void Move(Vector2 newPosition, float duration, Ease ease, bool isLoop)
        {
            var move = SelfTransform.DOLocalMove(newPosition, duration);

            move.SetRelative()
                .SetEase(ease);

            if (isLoop)
            {
                move.SetLoops(-1, LoopType.Yoyo);
            }
        }

        /// <summary>
        /// 移動する
        /// </summary>
        /// <param name="newPosition">新しい位置</param>
        /// <param name="duration">時間</param>
        /// <param name="isLoop">ループするか</param>
        public void Move(Vector2 newPosition, float duration, bool isLoop)
        {
            Move(newPosition, duration, DefaultEase, isLoop);
        }

        /// <summary>
        /// 移動する
        /// </summary>
        /// <param name="newPosition">新しい位置</param>
        /// <param name="duration">時間</param>
        public void Move(Vector2 newPosition, float duration)
        {
            Move(newPosition, duration, DefaultEase, false);
        }

        /// <summary>
        /// フェード処理を行う
        /// </summary>
        /// <param name="alpha">不透明度</param>
        /// <param name="duration">時間</param>
        public void Fade(float alpha, float duration)
        {
            if (!SelfCanvasGroup)
            {
                return;
            }

            SelfCanvasGroup.DOFade(alpha, duration);
        }

        /// <summary>
        /// フィルの量を変更する
        /// </summary>
        /// <param name="image">画像</param>
        /// <param name="amount">量</param>
        /// <param name="duration">時間</param>
        public void ChangeFillAmount(Image image, float amount, float duration)
        {
            DOTween.To(
                () => image.fillAmount,
                x => { image.fillAmount = x; },
                amount,
                duration);
        }

        /// <summary>
        /// 移動・フェードアウトを行い、最後に自身を非アクティブにする
        /// </summary>
        /// <param name="newPosition">座標</param>
        /// <param name="moveDuration">移動時間</param>
        /// <param name="staitonayDuration">静止時間</param>
        /// <param name="fadeDuration">フェード時間</param>
        public void MoveFadeAndDeactivate(FloatDisplaySetting settings)
        {
            if (!SelfCanvasGroup)
            {
                return;
            }

            // 不当明度初期化
            SelfCanvasGroup.alpha = InitialAlpha;

            // シーケンス
            Sequence sequence = DOTween.Sequence();

            // 移動
            var move = SelfTransform.DOLocalMove(settings.Position, settings.MoveDuration);
            move.SetRelative();
            sequence.Append(move);

            // 待機
            sequence.AppendInterval(settings.StationaryDuration);

            // 透明化
            var fade = SelfCanvasGroup.DOFade(Transparent, settings.FadeDuration);
            sequence.Append(fade);

            // 非アクティブ化
            sequence.OnComplete(() => gameObject.SetActive(false));
        }

        /// <summary>
        /// 点滅する
        /// </summary>
        public void Flash(FlashingSetting setting)
        {
            Sequence sequence = DOTween.Sequence();

            var fadeIn = SelfCanvasGroup.DOFade(1.0f, setting.FadeInDuration);
            sequence.Append(fadeIn);

            var fadeOut = SelfCanvasGroup.DOFade(0.0f, setting.FadeOutDuration);
            sequence.Append(fadeOut);
        }
    }
}
