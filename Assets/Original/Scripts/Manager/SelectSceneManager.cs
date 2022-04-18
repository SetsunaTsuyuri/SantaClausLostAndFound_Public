using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// セレクトシーンの管理者
    /// </summary>
    public class SelectSceneManager : SceneStateManagerBase
    {
        /// <summary>
        /// ステージ選択ボタンの配列
        /// </summary>
        public Button[] StageSelectButtons { get; private set; } = null;

        /// <summary>
        /// ステージ選択ボタンのレイアウトグループ
        /// </summary>
        [field: SerializeField]
        public ButtonRayoutGroup StageSelectButtonRayoutGroup { get; protected set; } = null;

        protected override void CacheComponents()
        {
            base.CacheComponents();

            GameObject buttonRayoutGroupObject = GameObject.Find(Glossary.Button.StageSelectButtonRayoutGroup);
            StageSelectButtonRayoutGroup = buttonRayoutGroupObject.GetComponent<ButtonRayoutGroup>();
        }

        protected override IEnumerator OnStartStartPhase()
        {
            // 敵・箱の総数・撃破・回収数初期化
            GameController.Instance.InitGameData();

            // ボタン生成
            CreateStageSelectButtons();

            // 初期化
            StageSelectButtonRayoutGroup.Init();

            yield return base.OnStartStartPhase();

            // 次のフェイズへ
            ChangeIntoNextPhase();
        }

        /// <summary>
        /// ステージ選択ボタンを生成する
        /// </summary>
        private void CreateStageSelectButtons()
        {
            StageData[] stageDataArray = ManagersMaster.Instance.FieldM.StageDataArray;
            int count = stageDataArray.Length;
            StageSelectButtons = new Button[count];
            for (int i = 0; i < count; i++)
            {
                Button button = CreateButton(stageDataArray[i].Name, StageSelectButtonRayoutGroup.transform);

                ButtonEvent buttonEvent = button.GetComponent<ButtonEvent>();
                int parameter = i;
                button.onClick.AddListener(() => buttonEvent.SelectStage(parameter));

                StageSelectButtons[i] = button;
            }
        }

        /// <summary>
        /// ボタンを生成する
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="parent">親</param>
        private Button CreateButton(string name, Transform parent)
        {
            // ボタンを生成する
            Button buttonPrefab = ManagersMaster.Instance.UIM.StageSelectButtonPrefab;
            Button buttonObject = Instantiate(buttonPrefab);
            buttonObject.transform.SetParent(parent, false);

            // ステージ名を表示するようにする
            Text buttonText = buttonObject.transform.Find(Glossary.UI.Text).GetComponent<Text>();
            buttonText.text = name;

            return buttonObject;
        }
    }
}
