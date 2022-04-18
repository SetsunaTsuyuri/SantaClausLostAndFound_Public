using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

namespace DungeonRPG3D
{
    /// <summary>
    /// 用語集
    /// </summary>
    public static class Glossary
    {
        /// <summary>
        /// 効果音
        /// </summary>
        public static class SE
        {
            public static readonly string PlayerDamage = SEPath.PUNCHHIGH2; 
        }

        /// <summary>
        /// タグ
        /// </summary>
        public static class Tag
        {
            public static readonly string Player = "Player";
        }

        /// <summary>
        /// シーン
        /// </summary>
        public static class Scene
        {
            /// <summary>
            /// タイトル
            /// </summary>
            public static readonly string Title = "Title";

            /// <summary>
            /// ステージセレクト
            /// </summary>
            public static readonly string Select = "Select";

            /// <summary>
            /// ゲーム
            /// </summary>
            public static readonly string Game = "Game";

            /// <summary>
            /// 結果
            /// </summary>
            public static readonly string Result = "Result";
        }

        /// <summary>
        /// カメラ
        /// </summary>
        public static class Camera
        {
            /// <summary>
            /// 冒険カメラの追跡対象
            /// </summary>
            public static readonly string AdventureVirtualCameraTarget = "AdventureVirtualCameraTarget";

            /// <summary>
            /// 戦闘カメラの追跡対象
            /// </summary>
            public static readonly string BattleVirtualCameraTarget = "BattleVirtualCameraTarget";
        }

        /// <summary>
        /// UI
        /// </summary>
        public static class UI
        {
            /// <summary>
            /// テキスト
            /// </summary>
            public static readonly string Text = "Text";

            /// <summary>
            /// 獲得経験値表示の接頭辞
            /// </summary>
            public static readonly string ExperienceDisplayPrefix = "経験値 + ";

            /// <summary>
            /// フィールドレベル表示の接尾辞
            /// </summary>
            public static readonly string CurrentFiledLevelSuffix = " F";
        }

        /// <summary>
        /// ボタン
        /// </summary>
        public static class Button
        {
            /// <summary>
            /// ニューゲームボタン
            /// </summary>
            public static readonly string NewGameButton = "NewGameButton";

            /// <summary>
            /// 終了ボタン
            /// </summary>
            public static readonly string ExitButton = "ExitButton";

            /// <summary>
            /// モード選択ボタンのレイアウトグループ
            /// </summary>
            public static readonly string ModeSelectButtonRayoutGroup = "ModeSelectButtonRayoutGroup";

            /// <summary>
            /// ステージ選択ボタンのレイアウトグループ
            /// </summary>
            public static readonly string StageSelectButtonRayoutGroup = "StageSelectButtonRayoutGroup";
        }

        /// <summary>
        /// 戦闘計算式
        /// </summary>
        public static class Calculation
        {
            /// <summary>
            /// 計算しない
            /// </summary>
            public static readonly string None = "";

            /// <summary>
            /// 固定ダメージ
            /// </summary>
            public static readonly string TrueDamage = "固定";

            /// <summary>
            /// 物理ダメージ
            /// </summary>
            public static readonly string Physical = "物理";

            /// <summary>
            /// 魔法ダメージ
            /// </summary>
            public static readonly string Magical = "魔法";
        }

        /// <summary>
        /// 属性
        /// </summary>
        public static class Attribute
        {
            /// <summary>
            /// 無属性
            /// </summary>
            public static readonly string None = "";

            /// <summary>
            /// 斬属性
            /// </summary>
            public static readonly string Slashing = "斬";

            /// <summary>
            /// 突属性
            /// </summary>
            public static readonly string Stabbing = "突";

            /// <summary>
            /// 打属性
            /// </summary>
            public static readonly string Blow = "打";

            /// <summary>
            /// 炎属性
            /// </summary>
            public static readonly string Fire = "<color=#ff0000>炎</color>";

            /// <summary>
            /// 氷属性
            /// </summary>
            public static readonly string Ice = "<color=#0000ff>氷</color>";

            /// <summary>
            /// 雷属性
            /// </summary>
            public static readonly string Thunder = "<color=#ffff00>雷</color>";

        }

        /// <summary>
        /// ステータス
        /// </summary>
        public static class Status
        {
            /// <summary>
            /// レベル
            /// </summary>
            public static readonly string Level = "<color=#ffd700>LV</color>";

            /// <summary>
            /// 威力
            /// </summary>
            public static readonly string Power = "<color=#ffd700>攻撃</color>";

            /// <summary>
            /// 回復
            /// </summary>
            public static readonly string Recovery = "<color=#ffd700>回復</color>";

            /// <summary>
            /// 守備力
            /// </summary>
            public static readonly string DefensivePower = "<color=#ffd700>守備</color>";

            /// <summary>
            /// チャージタイム
            /// </summary>
            public static readonly string ChargeTime = "<color=#ffd700>待機</color>";
        }
    }
}
