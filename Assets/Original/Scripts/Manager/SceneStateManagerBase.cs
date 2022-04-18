using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 各シーンの管理者の基底クラス
    /// </summary>
    public abstract class SceneStateManagerBase : ManagerBase
    {
        /// <summary>
        /// フェイズ
        /// </summary>
        public enum Phase
        {
            None,
            Start,
            Play
        }

        /// <summary>
        /// 現在のフェイズ
        /// </summary>
        public Phase CurrentPhase { get; protected set; } = Phase.None;

        protected override void StartInner()
        {
            base.StartInner();

            ChangeIntoNextPhase();
        }

        /// <summary>
        /// 現在のフェイズを変更する
        /// </summary>
        /// <param name="newPhase">新しいフェイズ</param>
        public void ChangePhase(Phase newPhase)
        {
            if (CurrentPhase == newPhase)
            {
                return;
            }

            CurrentPhase = newPhase;
            OnStartNewPhase();
        }

        /// <summary>
        /// 次のフェイズに移行する
        /// </summary>
        public void ChangeIntoNextPhase()
        {
            if (CurrentPhase == Phase.Play)
            {
                ChangePhase(Phase.None);
            }
            else
            {
                ChangePhase(CurrentPhase + 1);
            }
        }

        /// <summary>
        /// フェイズが変更された際、最初に行う処理
        /// </summary>
        protected virtual void OnStartNewPhase()
        {
            switch (CurrentPhase)
            {
                case Phase.Start:
                    StartCoroutine(OnStartStartPhase());
                    break;
            }
        }

        /// <summary>
        /// スタートフェイズ開始時の処理(コルーチン)
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator OnStartStartPhase()
        {
            // フェードイン
            yield return ManagersMaster.Instance.UIM.DoFadeIn();
        }
    }
}
