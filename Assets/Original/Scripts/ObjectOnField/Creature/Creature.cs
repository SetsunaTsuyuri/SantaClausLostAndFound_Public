using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物
    /// </summary>
    public abstract class Creature : ObjectOnField
    {
        /// <summary>
        /// 体の向き
        /// </summary>
        public enum Direction
        {
            North = 0,
            East = 90,
            West = -90,
            South = 180
        }

        /// <summary>
        /// ステート
        /// </summary>
        public enum State
        {
            None,
            Idle,
            Move,
            PreparingForBattle,
            InBattle,
            Dead
        }

        /// <summary>
        /// 現在のステート
        /// </summary>
        public State CurrentState { get; protected set; } = State.None;

        /// <summary>
        /// 配列何番目のデータを読み込むか
        /// </summary>
        [field: SerializeField]
        public int ID { get; protected set; } = 0;

        /// <summary>
        /// 移動
        /// </summary>
        [field: SerializeField, HideInInspector]
        public CreatureMove SelfMove { get; protected set; } = null;

        /// <summary>
        /// 戦闘
        /// </summary>
        [field: SerializeField, HideInInspector]
        public CreatureBattle SelfBattle { get; protected set; } = null;

        /// <summary>
        /// アニメーション
        /// </summary>
        [field: SerializeField, HideInInspector]
        public CreatureAnimation SelfAnimation { get; protected set; } = null;

        /// <summary>
        /// 戦闘カメラに追跡させる対象
        /// </summary>
        [field: SerializeField, HideInInspector]
        public Transform BattleVirtualCameraTarget { get; protected set; } = null;

        protected override void ChacheComponents()
        {
            base.ChacheComponents();

            SelfMove = GetComponent<CreatureMove>();
            SelfBattle = GetComponent<CreatureBattle>();
            BattleVirtualCameraTarget = SelfTransform.Find(Glossary.Camera.BattleVirtualCameraTarget);

            SelfMove.ChacheComponents();
            SelfBattle.ChacheComponents();
        }

        public override void Init()
        {
            base.Init();

            // モデル生成
            Instantiate(GetData().Model, SelfTransform);

            // コンポーネント取得
            SelfAnimation = GetComponentInChildren<CreatureAnimation>();

            // 戦闘の初期化
            SelfBattle.Init();

            // 待機状態になる
            ChangeState(State.Idle);
        }

        /// <summary>
        /// 現在のステートを変更する
        /// </summary>
        /// <param name="newState">新しいステート</param>
        public virtual void ChangeState(State newState)
        {
            if (CurrentState == newState)
            {
                return;
            }

            CurrentState = newState;

            OnStartNewState();
        }

        /// <summary>
        /// ステートが変更された際、最初に行う処理
        /// </summary>
        private void OnStartNewState()
        {
            switch (CurrentState)
            {
                case State.Idle:
                    OnStartIdleState();
                    break;

                case State.Move:
                    OnStartMoveState();
                    break;

                case State.InBattle:
                    OnStartInBattleState();
                    break;
            }
        }

        /// <summary>
        /// 「待機」ステート開始時に行う処理
        /// </summary>
        protected virtual void OnStartIdleState() { }

        /// <summary>
        /// 「移動」ステート開始時に行う処理
        /// </summary>
        protected virtual void OnStartMoveState()
        {
            SelfAnimation.Play(CreatureAnimation.ID.Move);
        }

        /// <summary>
        /// 「戦闘中」ステート開始時に行う処理
        /// </summary>
        protected virtual void OnStartInBattleState()
        {
            SelfAnimation.Play(CreatureAnimation.ID.Idle);
        }

        /// <summary>
        /// 「死亡」ステート開始時に行う処理
        /// </summary>
        protected virtual void OnStartDeadState()
        {
        }

        private void Update()
        {
            UpdateInner();
        }

        /// <summary>
        /// 1フレームに1度行う処理
        /// </summary>
        protected virtual void UpdateInner()
        {
            switch (CurrentState)
            {
                case State.Idle:
                    UpdateOnIdleState();
                    break;

                case State.Move:
                    UpdateOnMoveState();
                    break;
            }
        }

        /// <summary>
        /// 「待機」状態時の更新処理
        /// </summary>
        protected virtual void UpdateOnIdleState()
        {
            if (!SelfMove.IsMoving())
            {
                SelfAnimation.Play(CreatureAnimation.ID.Idle);
            }
        }

        /// <summary>
        /// 「移動」状態時の更新処理
        /// </summary>
        protected virtual void UpdateOnMoveState() { }

        /// <summary>
        /// 死亡しているか
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            return CurrentState == State.Dead;
        }

        /// <summary>
        /// 自分のデータを取得する
        /// </summary>
        /// <returns>生物データ</returns>
        public abstract CreatureData GetData();

        /// <summary>
        /// IDを設定する
        /// </summary>
        /// <param name="newID">新しいID</param>
        public void SetID(int newID)
        {
            ID = newID;
        }
    }
}
