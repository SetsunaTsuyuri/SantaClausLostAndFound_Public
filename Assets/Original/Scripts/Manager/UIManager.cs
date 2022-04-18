using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// UIの管理者
    /// </summary>
    public class UIManager : ManagerBase
    {
        /// <summary>
        /// ステージ選択ボタンのプレファブ
        /// </summary>
        [field: SerializeField]
        public Button StageSelectButtonPrefab { get; private set; } = null;

        /// <summary>
        /// 浮かぶ表示オブジェクトをいくつ作るか
        /// </summary>
        [field: SerializeField]
        public int FloatDisplayPool { get; private set; } = 5;

        /// <summary>
        /// 獲得経験値表示のプレファブ
        /// </summary>
        [field: SerializeField]
        public FloatDisplay ObtainedExperienceDisplayPrefab { get; private set; } = null;

        /// <summary>
        /// 獲得経験値表示の親オブジェクト
        /// </summary>
        [field: SerializeField]
        public RectTransform ObtainedExperienceDisplayParent { get; private set; } = null;

        /// <summary>
        /// 経験値取得表示の配列
        /// </summary>
        public FloatDisplay[] ObtainedExperienceDisplayArray { get; private set; } = { };

        /// <summary>
        /// 敵のダメージ及び回復表示の親オブジェクト
        /// </summary>
        [field: SerializeField]
        public RectTransform EnemyDamgeAndHealingDisplayParent { get; private set; } = null;

        /// <summary>
        /// 敵のダメージ表示のプレファブ
        /// </summary>
        [field: SerializeField]
        public FloatDisplay EnemyDamageDisplayPrefab { get; private set; } = null;

        /// <summary>
        /// 敵のダメージ表示の配列
        /// </summary>
        public FloatDisplay[] EnemyDamageDisplayArray { get; private set; } = { };

        /// <summary>
        /// 敵の回復表示のプレファブ
        /// </summary>
        [field: SerializeField]
        public FloatDisplay EnemyHealingDisplayPrefab { get; private set; } = null;

        /// <summary>
        /// 敵の回復表示の配列
        /// </summary>
        public FloatDisplay[] EnemyHealingDisplayArray { get; private set; } = { };

        /// <summary>
        /// プレイヤーのダメージ及び回復表示の親オブジェクト
        /// </summary>
        [field: SerializeField]
        public RectTransform PlayerDamageAndHealingDisplayParent { get; private set; } = null;

        /// <summary>
        /// プレイヤーのダメージ表示のプレファブ
        /// </summary>
        [field: SerializeField]
        public FloatDisplay PlayerDamageDisplayPrefab { get; private set; } = null;

        /// <summary>
        /// プレイヤーのダメージ表示の配列
        /// </summary>
        public FloatDisplay[] PlayerDamageDisplayArray { get; private set; } = { };

        /// <summary>
        /// プレイヤーの回復表示のプレファブ
        /// </summary>
        [field: SerializeField]
        public FloatDisplay PlayerHealingDisplayPrefab { get; private set; } = null;

        /// <summary>
        /// プレイヤーの回復表示の配列
        /// </summary>
        public FloatDisplay[] PlayerHealingDisplayArray { get; private set; } = { };

        /// <summary>
        /// プレイヤー行動パネルのフラッシュ
        /// </summary>
        [field: SerializeField]
        public Flashing PlayerActionPanelFlashing { get; private set; } = null;

        /// <summary>
        /// 敵行動パネルのフラッシュ
        /// </summary>
        [field: SerializeField]
        public Flashing EnemyActionPanelFlashing { get; private set; } = null;

        /// <summary>
        /// フェード
        /// </summary>
        [field: SerializeField]
        public UIAnimation Fade { get; private set; } = null;

        /// <summary>
        /// フェード処理にかかる時間
        /// </summary>
        [field: SerializeField]
        public float FadeDuration { get; private set; } = 0.25f;

        /// <summary>
        /// 冒険画面のキャンバス
        /// </summary>
        [field: SerializeField]
        public Canvas AdventureCanvas { get; private set; } = null;

        /// <summary>
        /// 冒険画面:底部
        /// </summary>
        [field: SerializeField]
        public UIAnimation AdventureBottom { get; private set; } = null;

        /// <summary>
        /// 冒険画面:頂部
        /// </summary>
        [field: SerializeField]
        public UIAnimation AdventureTop { get; private set; } = null;

        /// <summary>
        /// バトル画面:底部
        /// </summary>
        [field: SerializeField]
        public UIAnimation BattleBottom { get; private set; } = null;

        /// <summary>
        /// バトル画面:頂部
        /// </summary>
        [field: SerializeField]
        public UIAnimation BattleTop { get; private set; } = null;

        /// <summary>
        /// バトル画面:右
        /// </summary>
        [field: SerializeField]
        public UIAnimation BattleRight { get; private set; } = null;

        /// <summary>
        /// 冒険・戦闘画面UIの移動時間
        /// </summary>
        [field: SerializeField]
        public float UIMoveDuration { get; private set; } = 0.3f;

        /// <summary>
        /// 百分率パネルの配列
        /// </summary>
        [field: SerializeField]
        public UIAnimation[] RatePanels { get; private set; } = null;

        /// <summary>
        /// 百分率パネルの移動時間
        /// </summary>
        [field: SerializeField]
        public float RatePanelMoveDuration { get; private set; } = 0.5f;

        /// <summary>
        /// 百分率パネルの移動開始の際に発生する遅延時間
        /// </summary>
        [field: SerializeField]
        public float RatePanelMoveDelay { get; private set; } = 0.1f;

        public override void Init()
        {
            base.Init();

            InitAllUIAnimations();

            CreateFloatDisplays();
        }

        /// <summary>
        /// 全てのUIアニメを初期化する
        /// </summary>
        private void InitAllUIAnimations()
        {
            if (Fade)
            {
                Fade.Init();
            }

            if (BattleBottom)
            {
                BattleBottom.Init();
            }

            if (BattleTop)
            {
                BattleTop.Init();
            }
        }

        /// <summary>
        /// 浮かぶ表示を非アクティブで生成する
        /// </summary>
        public void CreateFloatDisplays()
        {
            ObtainedExperienceDisplayArray = new FloatDisplay[FloatDisplayPool];
            for (int i = 0; i < ObtainedExperienceDisplayArray.Length; i++)
            {
                FloatDisplay display = Instantiate(ObtainedExperienceDisplayPrefab, ObtainedExperienceDisplayParent);
                ObtainedExperienceDisplayArray[i] = display;
            }

            EnemyDamageDisplayArray = new FloatDisplay[FloatDisplayPool];
            for (int i = 0; i < EnemyDamageDisplayArray.Length; i++)
            {
                FloatDisplay display = Instantiate(EnemyDamageDisplayPrefab, EnemyDamgeAndHealingDisplayParent);
                EnemyDamageDisplayArray[i] = display;
            }

            EnemyHealingDisplayArray = new FloatDisplay[FloatDisplayPool];
            for (int i = 0; i < EnemyDamageDisplayArray.Length; i++)
            {
                FloatDisplay display = Instantiate(EnemyHealingDisplayPrefab, EnemyDamgeAndHealingDisplayParent);
                EnemyHealingDisplayArray[i] = display;
            }

            PlayerDamageDisplayArray = new FloatDisplay[FloatDisplayPool];
            for (int i = 0; i < PlayerDamageDisplayArray.Length; i++)
            {
                FloatDisplay display = Instantiate(PlayerDamageDisplayPrefab, PlayerDamageAndHealingDisplayParent);
                PlayerDamageDisplayArray[i] = display;
            }

            PlayerHealingDisplayArray = new FloatDisplay[FloatDisplayPool];
            for (int i = 0; i < PlayerDamageDisplayArray.Length; i++)
            {
                FloatDisplay display = Instantiate(PlayerHealingDisplayPrefab, PlayerDamageAndHealingDisplayParent);
                PlayerHealingDisplayArray[i] = display;
            }
        }

        /// <summary>
        /// フェードアウトコルーチン
        /// </summary>
        /// <returns></returns>
        public IEnumerator DoFadeOut()
        {
            yield return DoFadeOut(FadeDuration);
        }

        /// <summary>
        /// フェードアウトコルーチン
        /// </summary>
        /// <param name="duration">時間</param>
        /// <returns></returns>
        public IEnumerator DoFadeOut(float duration)
        {
            Fade.Fade(UIAnimation.FadeOut, duration);
            yield return new WaitForSeconds(duration);
        }

        /// <summary>
        /// フェードインコルーチン
        /// </summary>
        /// <returns></returns>
        public IEnumerator DoFadeIn()
        {
            yield return DoFadeIn(FadeDuration);
        }

        /// <summary>
        /// フェードインコルーチン
        /// </summary>
        /// <param name="duration">時間</param>
        /// <returns></returns>
        public IEnumerator DoFadeIn(float duration)
        {
            Fade.Fade(UIAnimation.FadeIn, duration);
            yield return new WaitForSeconds(duration);
        }

        /// <summary>
        /// 「戦闘開始」開始時のコルーチン
        /// </summary>
        /// <returns></returns>
        public IEnumerator OnStartBeginingBattle()
        {
            // 底部冒険UI移動
            Vector2 moveBottom = new Vector2(0.0f, -AdventureBottom.SelfTransform.rect.height);
            AdventureBottom.Move(moveBottom, UIMoveDuration);

            // 頂部冒険UI移動
            Vector2 moveTop = new Vector2(0.0f, AdventureTop.SelfTransform.rect.height);
            AdventureTop.Move(moveTop, UIMoveDuration);

            // フェードアウト
            yield return DoFadeOut();
        }

        /// <summary>
        /// 「戦闘開始」終了時のコルーチン
        /// </summary>
        /// <returns></returns>
        public IEnumerator OnEndBeginingBattle()
        {
            // 底部戦闘UI移動
            Vector2 moveBottom = new Vector2(0.0f, BattleBottom.SelfTransform.rect.height);
            BattleBottom.Move(moveBottom, UIMoveDuration);

            // 頂部戦闘UI移動
            Vector2 moveTop = new Vector2(0.0f, -BattleTop.SelfTransform.rect.height);
            BattleTop.Move(moveTop, UIMoveDuration);

            // 右戦闘UI移動
            Vector2 moveRight = new Vector2(-BattleRight.SelfTransform.rect.width, 0.0f);
            BattleRight.Move(moveRight, UIMoveDuration);

            // フェードイン
            yield return DoFadeIn();
        }

        /// <summary>
        /// 「戦闘終了」開始時のコルーチン
        /// </summary>
        /// <returns></returns>
        public IEnumerator OnStartBattleEnd()
        {
            // 底部戦闘UI移動
            Vector2 moveBottom = new Vector2(0.0f, -BattleBottom.SelfTransform.rect.height);
            BattleBottom.Move(moveBottom, UIMoveDuration);

            // 頂部戦闘UI移動
            Vector2 moveTop = new Vector2(0.0f, BattleTop.SelfTransform.rect.height);
            BattleTop.Move(moveTop, UIMoveDuration);

            // 右UI戦闘移動
            Vector2 moveRight = new Vector2(BattleRight.SelfTransform.rect.width, 0.0f);
            BattleRight.Move(moveRight, UIMoveDuration);

            // フェードアウト
            yield return DoFadeOut();
        }

        /// <summary>
        /// 「プレイヤー勝利」終了時のコルーチン
        /// </summary>
        /// <returns></returns>
        public IEnumerator OnEndPlayerHasWon()
        {
            // 底部冒険UI移動
            Vector2 moveBottom = new Vector2(0.0f, AdventureBottom.SelfTransform.rect.height);
            AdventureBottom.Move(moveBottom, UIMoveDuration);

            // 頂部冒険UI移動
            Vector2 moveTop = new Vector2(0.0f, -AdventureTop.SelfTransform.rect.height);
            AdventureTop.Move(moveTop, UIMoveDuration);

            // フェードイン
            yield return DoFadeIn();
        }

        /// <summary>
        /// リザルトシーン開始時のコルーチン
        /// </summary>
        /// <returns></returns>
        public IEnumerator OnStartResultScene()
        {
            yield return null;
        }

        /// <summary>
        /// 獲得経験値を表示する
        /// </summary>
        /// <param name="value">経験値</param>
        /// <param name="newPosition">表示する座標</param>
        public void DisplayObtainedExperience(int value, Vector3 newPosition)
        {
            FloatDisplay display = GetFloatDisplay(ObtainedExperienceDisplayArray);
            string suffix = Glossary.UI.ExperienceDisplayPrefix;

            display.SetText(suffix, value);
            display.SetPosition(newPosition);
            display.gameObject.SetActive(true);
        }

        /// <summary>
        /// 生物のダメージを表示する
        /// </summary>
        /// <param name="creatureBattle">生物(戦闘)</param>
        /// <param name="value">ダメージ</param>
        public void DisplayDamage(CreatureBattle creatureBattle, int value)
        {
            FloatDisplay display;
            if (creatureBattle is PlayerBattle)
            {
                display = GetFloatDisplay(PlayerDamageDisplayArray);
            }
            else
            {
                display = GetFloatDisplay(EnemyDamageDisplayArray);
            }

            display.SetText(value);
            display.InitPosition();
            display.gameObject.SetActive(true);
        }

        /// <summary>
        /// 生物の回復量を表示する
        /// </summary>
        /// <param name="creatureBattle">生物(戦闘)</param>
        /// <param name="value">回復量</param>
        public void DisplayHealing(CreatureBattle creatureBattle ,int value)
        {
            FloatDisplay display;
            if (creatureBattle is PlayerBattle)
            {
                display = GetFloatDisplay(PlayerHealingDisplayArray);
            }
            else
            {
                display = GetFloatDisplay(EnemyHealingDisplayArray);
            }

            display.SetText(value);
            display.InitPosition();
            display.gameObject.SetActive(true);
        }

        /// <summary>
        /// 浮かぶ表示を取得する
        /// </summary>
        /// <param name="displays">表示の配列</param>
        /// <param name="suffix">接頭辞</param>
        /// <param name="value">表示する値</param>
        /// <param name="position">座標</param>
        private FloatDisplay GetFloatDisplay(FloatDisplay[] displays)
        {
            FloatDisplay floatDisplayToBeActivated = null;

            foreach (var val in displays)
            {
                if (!val.isActiveAndEnabled)
                {
                    floatDisplayToBeActivated = val;
                    break;
                }
            }

            if (floatDisplayToBeActivated == null)
            {
                return null;
            }

            return floatDisplayToBeActivated;
        }

        /// <summary>
        /// 戦闘関連の浮かぶ表示を非アクティブにする
        /// </summary>
        public void DeactivateBattleFloatDisplays()
        {
            foreach (var display in EnemyDamageDisplayArray)
            {
                if (display.isActiveAndEnabled)
                {
                    display.gameObject.SetActive(false);
                }
            }

            foreach (var display in EnemyHealingDisplayArray)
            {
                if (display.isActiveAndEnabled)
                {
                    display.gameObject.SetActive(false);
                }
            }

            foreach (var display in PlayerDamageDisplayArray)
            {
                if (display.isActiveAndEnabled)
                {
                    display.gameObject.SetActive(false);
                }
            }

            foreach (var display in PlayerHealingDisplayArray)
            {
                if (display.isActiveAndEnabled)
                {
                    display.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// 行動パネルを点滅させる
        /// </summary>
        /// <param name="creatureBattle">生物(戦闘)</param>
        public IEnumerator FlashActionPanel(CreatureBattle  creatureBattle)
        {
            if (creatureBattle is PlayerBattle)
            {
                yield return PlayerActionPanelFlashing.Flash();
            }
            else
            {
                yield return EnemyActionPanelFlashing.Flash();
            }
        }
    }
}
