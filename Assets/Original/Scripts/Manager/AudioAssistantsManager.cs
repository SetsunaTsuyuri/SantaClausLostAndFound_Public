using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using KanKikuchi.AudioManager;

namespace DungeonRPG3D
{
    /// <summary>
    /// BGMAssistant及びSEAssistantの管理者
    /// </summary>
    public class AudioAssistantsManager : ManagerBase
    {
        /// <summary>
        /// フォーカスが切り替わったときのSE
        /// </summary>
        [field: SerializeField]
        public SEAssistant FocusChange { get; private set; } = null;

        /// <summary>
        /// プレイヤーダメージSE
        /// </summary>
        [field: SerializeField]
        public SEAssistant PlayerDamage { get; private set; } = null;

        /// <summary>
        /// 敵ダメージSE
        /// </summary>
        [field: SerializeField]
        public SEAssistant EnemyDamage { get; private set; } = null;

        /// <summary>
        /// 回復SE
        /// </summary>
        [field: SerializeField]
        public SEAssistant Healing { get; private set; } = null;

        /// <summary>
        /// プレイヤー行動開始SE
        /// </summary>
        [field: SerializeField]
        public SEAssistant StartPlayerAction { get; private set; } = null;

        /// <summary>
        /// 敵行動開始SE
        /// </summary>
        [field: SerializeField]
        public SEAssistant StartEnemyAction { get; private set; } = null;

        /// <summary>
        /// 経験値獲得SE
        /// </summary>
        [field: SerializeField]
        public SEAssistant ObtainingExperience { get; private set; } = null;

        /// <summary>
        /// 前のフレームで選択されていたゲームオブジェクト
        /// </summary>
        public GameObject SelectedGameObjectInPreviousFrame { get; private set; } = null;

        protected override void CacheComponents()
        {
            base.CacheComponents();

            Transform se = transform.Find("SE");
            FocusChange = se.Find("FocusChange").GetComponent<SEAssistant>();
            PlayerDamage = se.Find("PlayerDamage").GetComponent<SEAssistant>();
            EnemyDamage = se.Find("EnemyDamage").GetComponent<SEAssistant>();
            Healing = se.Find("Healing").GetComponent<SEAssistant>();
            StartPlayerAction = se.Find("StartPlayerAction").GetComponent<SEAssistant>();
            StartEnemyAction = se.Find("StartEnemyAction").GetComponent<SEAssistant>();
            ObtainingExperience = se.Find("ObtainingExperience").GetComponent<SEAssistant>();
        }

        public override void Init()
        {
            base.Init();

            PlayBGM();
        }

        /// <summary>
        /// BGMを再生する
        /// </summary>
        private void PlayBGM()
        {
            // BGM継続フラグがOFFなら中止
            if (GameController.Instance.ContinueBGM)
            {
                return;
            }

            // BGM取得
            FieldManager fieldManager = ManagersMaster.Instance.FieldM;
            BGMAssistant bgm = fieldManager.CurrentField.BGM;

            if (bgm != null)
            {
                bgm.Play();
            }
        }

        protected override void UpdateInner()
        {
            base.UpdateInner();

            if (CheckFocusChange(out GameObject currentSelectedGameObject))
            {
                PlayFocusChangeSE(currentSelectedGameObject);
            }

        }

        private bool CheckFocusChange(out GameObject currentSelectedGameObject)
        {
            bool result = false;

            // フォーカス変化による効果音再生
            currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
            if (currentSelectedGameObject != null &&
                currentSelectedGameObject != SelectedGameObjectInPreviousFrame)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// フォーカス変化による効果音を再生する
        /// </summary>
        private void PlayFocusChangeSE(GameObject currentSelectedGameObject)
        {
            if (SelectedGameObjectInPreviousFrame != null)
            {
                FocusChange.Play();
            }
            SelectedGameObjectInPreviousFrame = currentSelectedGameObject;
        }
    }
}
