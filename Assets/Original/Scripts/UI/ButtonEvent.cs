using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

namespace DungeonRPG3D
{
    /// <summary>
    /// ボタンを押したときに起きるイベント
    /// </summary>
    public class ButtonEvent : MonoBehaviour
    {
        /// <summary>
        /// 1度しか押せないか
        /// </summary>
        [field: SerializeField]
        public bool CanOnlyBePressedOnce { get; private set; } = false;

        /// <summary>
        /// ボタン
        /// </summary>
        [field: SerializeField]
        public Button SelfButton { get; private set; } = null;

        /// <summary>
        /// 効果音
        /// </summary>
        [field: SerializeField]
        public SEAssistant SelfSE { get; private set; } = null;

        private void Reset()
        {
            CacheComponents();
        }

        /// <summary>
        /// 必要なコンポーネントをメンバ変数にキャッシュする
        /// </summary>
        private void CacheComponents()
        {
            SelfButton = GetComponent<Button>();
            SelfSE = GetComponent<SEAssistant>();
        }

        /// <summary>
        /// ポインターが入ったときに起こる処理
        /// </summary>
        public void OnPointerEnter()
        {
            // 自身をフォーカスする
            SelfButton.Select();
        }

        /// <summary>
        /// ボタンが押されたときに起こる処理
        /// </summary>
        public void OnClick()
        {
            UpdateInteractable();
            PlaySE();
        }

        /// <summary>
        /// ボタンの選択可否状態を更新する
        /// </summary>
        private void UpdateInteractable()
        {
            // 1度しか押せないボタンの場合
            if (SelfButton.interactable && CanOnlyBePressedOnce)
            {
                // ボタンを無効にする
                SelfButton.interactable = false;
            }
        }

        /// <summary>
        /// 効果音を再生する
        /// </summary>
        private void PlaySE()
        {
            if (SelfSE)
            {
                SelfSE.Play();
            }
        }

        /// <summary>
        /// シーンを切り替える
        /// </summary>
        /// <param name="name">シーン名</param>
        public void ChangeScene(string name)
        {
            SceneChangeManager manager = ManagersMaster.Instance.SceneChangeM;
            StartCoroutine(manager.ChangeScene(name));
        }

        /// <summary>
        /// ゲームを終了する
        /// </summary>
        public void QuitGame()
        {
            GameController.Instance.QuitGame();
        }

        /// <summary>
        /// ステージを選択する
        /// </summary>
        /// <param name="index">配列何番目のステージか</param>
        public void SelectStage(int newIndex)
        {
            GameController.Instance.SelectStageData(newIndex);
        }
    }
}
