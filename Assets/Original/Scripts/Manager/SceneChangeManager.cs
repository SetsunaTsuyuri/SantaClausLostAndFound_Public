using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace DungeonRPG3D
{
    /// <summary>
    /// シーン変化の管理者
    /// </summary>
    public class SceneChangeManager : ManagerBase
    {
        /// <summary>
        /// シーンを切り替える
        /// </summary>
        /// <param name="name">シーンの名前</param>
        /// <returns></returns>
        public IEnumerator ChangeScene(string name)
        {
            // フェードアウト
            yield return ManagersMaster.Instance.UIM.DoFadeOut();

            // Tweenを全て殺す
            DOTween.KillAll();

            if (name == Glossary.Scene.Select)
            {

            }
            else if (name == Glossary.Scene.Game)
            {
                Player player = ManagersMaster.Instance.ObjectsOnFieldM.Player;

                SceneManager.sceneLoaded += OnGameSceneLoaded;
            }
            else if (name == Glossary.Scene.Result)
            {
                SceneManager.sceneLoaded += OnResultSceneLoaded;
            }

            SceneManager.LoadScene(name);
        }

        /// <summary>
        /// ゲームシーンへ移行した後に行う処理
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="loadSceneMode"></param>
        private void OnGameSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            // このメソッドを削除する
            SceneManager.sceneLoaded -= OnGameSceneLoaded;
        }

        /// <summary>
        /// リザルトシーンへ移行した後に行う処理
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="loadSceneMode"></param>
        private void OnResultSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            // プレイヤー削除
            DestroyPlayerObject();

            // このメソッドを削除する
            SceneManager.sceneLoaded -= OnResultSceneLoaded;
        }

        /// <summary>
        /// プレイヤーオブジェクトを消す
        /// </summary>
        private void DestroyPlayerObject()
        {
            GameObject playerObject = GameObject.FindWithTag(Glossary.Tag.Player);
            if (playerObject)
            {
                Destroy(playerObject);
            }
        }
    }
}
