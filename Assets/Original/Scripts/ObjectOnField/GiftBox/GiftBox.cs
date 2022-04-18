using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレゼント箱
    /// </summary>
    public class GiftBox : ObjectOnField
    {
        /// <summary>
        /// リスト何番目のデータを読み込むか
        /// </summary>
        [field: SerializeField]
        public int ID { get; private set; } = 0;

        public enum State
        {
            Uncollected = 0,
            Collected = 1
        }

        /// <summary>
        /// 現在のステート
        /// </summary>
        public State CurrentState { get; private set; } = State.Uncollected;

        /// <summary>
        /// ステートを変更する
        /// </summary>
        /// <param name="newState">新しいステート</param>
        public void ChangeState(State newState)
        {
            if (newState == CurrentState)
            {
                return;
            }

            CurrentState = newState;
        }

        /// <summary>
        /// 自身は回収済みか
        /// </summary>
        /// <returns></returns>
        public bool IsCollected()
        {
            return CurrentState == State.Collected;
        }

        public override void Init()
        {
            base.Init();

            // モデル生成
            Instantiate(GetData().Model, SelfTransform);
        }

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <returns>データ</returns>
        public GiftBoxData GetData()
        {
            return ManagersMaster.Instance.ObjectsOnFieldM.GiftBoxDataList.Data[ID];
        }

        /// <summary>
        /// IDを設定する
        /// </summary>
        /// <param name="newID">新しいID</param>
        public void SetID(int newID)
        {
            ID = newID;
        }

        public override void OnTouchedByPlayer()
        {
            base.OnTouchedByPlayer();

            // 獲得される
            BeObtained();

        }

        /// <summary>
        /// 獲得される
        /// </summary>
        public void BeObtained()
        {
            Player player = ManagersMaster.Instance.ObjectsOnFieldM.Player;
            int experience = GetData().ExperiencePoint;
            player.GainExeperience(experience);

            ChangeState(State.Collected);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 既に獲得されていた場合の処理
        /// </summary>
        public void OnBeingObtainedAlreay()
        {
            ChangeState(State.Collected);
            gameObject.SetActive(false);
        }
    }
}
