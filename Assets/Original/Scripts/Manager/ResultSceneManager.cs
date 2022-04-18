using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

namespace DungeonRPG3D
{
    /// <summary>
    /// リザルトシーンの管理者
    /// </summary>
    public class ResultSceneManager : SceneStateManagerBase
    {
        /// <summary>
        /// クリアBGM
        /// </summary>
        [field: SerializeField]
        public BGMAssistant ClearBGM { get; private set; } = null;

        /// <summary>
        /// ゲームオーバーBGM
        /// </summary>
        [field: SerializeField]
        public BGMAssistant GameOverBGM { get; private set; } = null;

        /// <summary>
        /// ゲームオーバー時のタイトルテキストの文字列
        /// </summary>
        [field: SerializeField]
        public string GameOverTitleTextString { get; private set; } = "ゲームオーバー";

        /// <summary>
        /// ゲームオーバー時のタイトルテキストの色
        /// </summary>
        [field: SerializeField]
        public Color GameOverTitleTextColor { get; private set; } = Color.red;

        /// <summary>
        /// ゲームオーバー時の背景マテリアル
        /// </summary>
        [field: SerializeField]
        public Material GameOverBackgroundMaterial { get; private set; } = null;

        /// <summary>
        /// タイトルテキスト
        /// </summary>
        [field: SerializeField]
        public Text TitleText { get; private set; } = null;

        /// <summary>
        /// 背景
        /// </summary>
        [field: SerializeField]
        public Renderer Background { get; private set; } = null;

        /// <summary>
        /// 取得率パネルオブジェクトの配列
        /// </summary>
        [field: SerializeField]
        public GameObject[] RatePanelObjects { get; private set; } = { };

        protected override IEnumerator OnStartStartPhase()
        {
            GameController.Instance.UpdateGameDataForResultScene();

            // プレイヤーが敗北している場合
            if (GameController.Instance.PlayerHasDefeated)
            {
                // パネル非表示化
                foreach (var val in RatePanelObjects)
                {
                    val.SetActive(false);
                }

                // テキスト変更
                TitleText.text = GameOverTitleTextString;

                // テキストの色変更
                TitleText.color = GameOverTitleTextColor;

                // 背景マテリアル変更
                Background.material = GameOverBackgroundMaterial;

                // ゲームオーバーBGM再生
                GameOverBGM.Play();
            }
            else
            {
                // クリアBGM再生
                ClearBGM.Play();
            }

            yield return base.OnStartStartPhase();

            UIManager manager = ManagersMaster.Instance.UIM;
            StartCoroutine(manager.OnStartResultScene());

        }
    }
}
