using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using KanKikuchi.AudioManager;

namespace DungeonRPG3D
{
    /// <summary>
    /// 戦闘の管理者
    /// </summary>
    public class BattleManager : ManagerBase
    {
        /// <summary>
        /// フェイズ
        /// </summary>
        public enum Phase
        {
            None = 0,
            Start = 1,
            Main = 2,
            PlayerAction = 3,
            EnemyAction = 4,
            EnemyItemSelect = 5,
            PlayerItemSelect = 6,
            PlayerStartCharging = 7,
            DecreasingChargeTime = 8,
            End = 9
        }

        /// <summary>
        /// 現在のフェイズ
        /// </summary>
        public Phase CurrentPhase { get; private set; } = Phase.None;

        /// <summary>
        /// 戦闘開始時のイベント
        /// </summary>
        [SerializeField]
        GameEvent onStartBattleEvent = null;

        /// <summary>
        /// プレイヤー行動フェイズ終了時のイベント
        /// </summary>
        [SerializeField]
        GameEvent onEndPlayerActionPhaseEvent = null;

        /// <summary>
        /// 敵行動フェイズ終了時のイベント
        /// </summary>
        [SerializeField]
        GameEvent onEndEnemyActionPhaseEvent = null;

        /// <summary>
        /// 戦闘終了時のイベント
        /// </summary>
        [SerializeField]
        GameEvent onEndBattle = null;

        /// <summary>
        /// プレイヤー
        /// </summary>
        public Player Player { get; private set; } = null;

        /// <summary>
        /// プレイヤー(戦闘)
        /// </summary>
        public PlayerBattle PlayerBattle { get; private set; } = null;

        /// <summary>
        /// 敵
        /// </summary>
        public Enemy Enemy { get; private set; } = null;

        /// <summary>
        /// 敵(戦闘)
        /// </summary>
        public EnemyBattle EnemyBattle { get; private set; } = null;

        /// <summary>
        /// 戦闘の設定
        /// </summary>
        [field: SerializeField]
        public BattleSetting BattleSetting { get; private set; } = null;

        public override void Init()
        {
            base.Init();

            // プレイヤー取得
            Player = ManagersMaster.Instance.ObjectsOnFieldM.Player;
            PlayerBattle = Player.SelfBattle as PlayerBattle;
        }

        /// <summary>
        /// 戦闘を開始する
        /// </summary>
        /// <param name="enemy">敵</param>
        public IEnumerator BeginBattle(Enemy enemy)
        {
            // 敵取得
            Enemy = enemy;
            EnemyBattle = enemy.SelfBattle as EnemyBattle;

            UIManager uiManager = ManagersMaster.Instance.UIM;
            CameraManger cameraManger = ManagersMaster.Instance.CameraM;

            // UIアニメーションコルーチン
            yield return uiManager.OnStartBeginingBattle();

            // イベント実行
            onStartBattleEvent.Raise();

            // 冒険画面のキャンバスを非アクティブにする
            uiManager.AdventureCanvas.gameObject.SetActive(false);

            // 敵を戦闘態勢に移行させる
            Enemy.ChangeState(Creature.State.InBattle);

            // 戦闘態勢ではない敵を非アクティブにする
            ObjectsOnFieldManager objManager = ManagersMaster.Instance.ObjectsOnFieldM;
            objManager.DeactivateEnemiesThatAreNotInBattle();

            // 敵をプレイヤーの方向に向かせる
            Transform playerTransform = PlayerBattle.Owner.SelfTransform;
            Enemy.SelfTransform.LookAt(playerTransform);

            // プレイヤーを敵の方向方向に向かせる
            Player.SelfTransform.LookAt(enemy.SelfTransform);

            if (Enemy.GetEnemyData().IsBoss)
            {
                BGMManager.Instance.Play(BGMPath.MAMBO2_JODO_CATHARSIS);
            }

            // 仮想カメラのターゲット設定
            CinemachineVirtualCamera battleVirtualCamera = cameraManger.BattleVirtualCamera;
            battleVirtualCamera.LookAt = Enemy.BattleVirtualCameraTarget;

            // 仮想カメラの距離調整
            Vector3 newPosition = Player.BattleVirtualCameraTarget.localPosition;
            newPosition.z += Enemy.GetEnemyData().FollowedTargetOffsetZ;
            Player.BattleVirtualCameraTarget.transform.localPosition = newPosition;

            // 戦闘カメラON
            cameraManger.BattleCamera.enabled = true;

            // 冒険カメラOFF
            cameraManger.AdventureCamera.enabled = false;

            // プレイヤーの装備アイテムリストをデータから代入する
            PlayerBattle.SetEquippedItemsFromData();

            // 敵の装備アイテムリストをデータから代入する
            EnemyBattle.SetEquippedItemsFromData();

            // 敵味方の対戦相手を設定する
            PlayerBattle.SetOpponent(EnemyBattle);
            EnemyBattle.SetOpponent(PlayerBattle);

            // プレイヤーアイテム選択
            PlayerBattle.SelectTheActionItemWithTheLowestNumber();

            // UIアニメーションコルーチン
            yield return uiManager.OnEndBeginingBattle();

            // スタートフェイズに移行する
            ChangePhase(Phase.Start);
        }

        /// <summary>
        /// 現在のフェイズを変更する
        /// </summary>
        /// <param name="newPhase">新しいフェイズ</param>
        private void ChangePhase(Phase newPhase)
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
        private void ChangeIntoNextPhase()
        {
            if (CurrentPhase == Phase.End)
            {
                return;
            }

            ChangePhase(CurrentPhase + 1);
        }

        /// <summary>
        /// フェイズが変更された際、最初に行う処理
        /// </summary>
        private void OnStartNewPhase()
        {
            switch (CurrentPhase)
            {
                case Phase.Start:
                    OnStartStartPhase();
                    break;

                case Phase.Main:
                    OnStartMainPhase();
                    break;

                case Phase.PlayerAction:
                    StartCoroutine(OnStartPlayerActionPhase());
                    break;

                case Phase.EnemyAction:
                    StartCoroutine(OnStartEnemyActionPhase());
                    break;

                case Phase.EnemyItemSelect:
                    StartCoroutine(OnStartEnemyItemSelectPhase());
                    break;

                case Phase.PlayerStartCharging:
                    StartCoroutine(OnStartPlayerStartChargingPhase());
                    break;

                case Phase.DecreasingChargeTime:
                    OnStartDecreasingChargeTimePhase();
                    break;

                case Phase.End:
                    StartCoroutine(OnStartEndPhase());
                    break;
            }
        }

        /// <summary>
        /// 戦闘開始フェイズ開始時の処理
        /// </summary>
        private void OnStartStartPhase()
        {
            // 次のフェイズに移行する
            ChangeIntoNextPhase();
        }

        /// <summary>
        /// メインフェイズ開始時の処理
        /// </summary>
        private void OnStartMainPhase()
        {
            // 敵味方のどちらかが死亡したら戦闘終了
            if (CheckEitherCreatureIsDead())
            {
                ChangePhase(Phase.End);
            }
            // プレイヤーの行動実行
            else if (PlayerBattle.IsReadyToExecuteAction())
            {
                ChangePhase(Phase.PlayerAction);
            }
            // 敵の行動実行
            else if (EnemyBattle.IsReadyToExecuteAction())
            {
                ChangePhase(Phase.EnemyAction);
            }
            // 敵のアイテム選択
            else if (EnemyBattle.IsReadyToSelectItem())
            {
                ChangePhase(Phase.EnemyItemSelect);
            }
            // プレイヤーのアイテム選択
            else if (PlayerBattle.IsReadyToSelectItem())
            {
                ChangePhase(Phase.PlayerItemSelect);
            }
            // チャージタイム減少
            else
            {
                ChangePhase(Phase.DecreasingChargeTime);
            }
        }

        /// <summary>
        /// プレイヤーか敵のどちらかが死んでいるか
        /// </summary>
        /// <returns></returns>
        private bool CheckEitherCreatureIsDead()
        {
            return PlayerBattle.IsDead() || EnemyBattle.IsDead();
        }


        /// <summary>
        /// プレイヤーの行動実行フェイズ開始時の処理
        /// </summary>
        private IEnumerator OnStartPlayerActionPhase()
        {
            // 行動実行
            yield return PlayerBattle.ExecuteAction();

            // イベント実行
            onEndPlayerActionPhaseEvent.Raise();

            // フェイズ移行
            ChangePhase(Phase.Main);
        }

        /// <summary>
        /// 敵の行動実行フェイズ開始時の処理
        /// </summary>
        private IEnumerator OnStartEnemyActionPhase()
        {
            // 行動実行
            yield return EnemyBattle.ExecuteAction();

            // イベント実行
            onEndEnemyActionPhaseEvent.Raise();

            // フェイズ移行
            ChangePhase(Phase.Main);
        }

        /// <summary>
        /// 敵のアイテム選択フェイズ開始時の処理
        /// </summary>
        private IEnumerator OnStartEnemyItemSelectPhase()
        {
            // 何番目のアイテムを使うか
            int? index = EnemyBattle.AI.SelectEquippedItemIndex();

            // 使用アイテム変更
            EnemyBattle.ChangeItemInUse(index);

            // チャージ開始
            yield return EnemyBattle.StartCharging();

            // フェイズ移行
            ChangePhase(Phase.Main);
        }

        /// <summary>
        /// プレイヤーのチャージ開始フェイズ開始時の処理
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnStartPlayerStartChargingPhase()
        {
            // チャージ開始
            yield return PlayerBattle.StartCharging();

            // フェイズ移行
            ChangePhase(Phase.Main);
        }

        /// <summary>
        /// チャージタイム減少フェイズ開始時の処理
        /// </summary>
        private void OnStartDecreasingChargeTimePhase()
        {
            // チャージタイム減少
            DecreaseChargeTimes();

            // フェイズ移行
            ChangePhase(Phase.Main);
        }

        /// <summary>
        /// 敵味方のチャージタイムを減少させる
        /// </summary>
        private void DecreaseChargeTimes()
        {
            // 残りチャージタイムが少ない方の数値がゼロになるよう、減らす量を決める
            int time = PlayerBattle.RemainingChargeTime;
            if (time > EnemyBattle.RemainingChargeTime)
            {
                time = EnemyBattle.RemainingChargeTime;
            }

            // 敵味方の残りチャージタイムを同じだけ減らす
            PlayerBattle.DecreaseChargeTime(time);
            EnemyBattle.DecreaseChargeTime(time);
        }

        /// <summary>
        /// 戦闘終了フェイズ開始時の処理
        /// </summary>
        private IEnumerator OnStartEndPhase()
        {
            // 共通処理
            yield return OnBattleEnd();

            // イベント実行
            onEndBattle.Raise();

            // プレイヤーが生きていれば勝利とする
            if (!PlayerBattle.IsDead())
            {
                yield return OnPlayerHasWon();
            }
            else
            {
                yield return OnPlayerHasDefeated();
            }

            // フェイズ移行
            ChangePhase(Phase.None);
        }

        /// <summary>
        /// プレイヤーが勝利した際に実行するコルーチン
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnPlayerHasWon()
        {
            // ボスBGM終了
            if (Enemy.GetEnemyData().IsBoss)
            {
                BGMManager.Instance.FadeOut();
            }


            UIManager uiManager = ManagersMaster.Instance.UIM;
            ObjectsOnFieldManager objManager = ManagersMaster.Instance.ObjectsOnFieldM;
            CameraManger cameraManger = ManagersMaster.Instance.CameraM;

            // 浮かぶ表示を非アクティブにする
            uiManager.DeactivateBattleFloatDisplays();

            // 死亡した敵を非アクティブにする
            Enemy.gameObject.SetActive(false);

            // 死亡していない敵をアクティブにする
            objManager.ActivateLivingEnemies();

            // 冒険画面のキャンバスをアクティブにする
            uiManager.AdventureCanvas.gameObject.SetActive(true);

            // 冒険カメラON
            cameraManger.AdventureCamera.enabled = true;

            // 戦闘カメラOFF
            cameraManger.BattleCamera.enabled = false;

            // UIコルーチン
            yield return uiManager.OnEndPlayerHasWon();

            // ボス戦後BGM
            if ((EnemyBattle.Owner as Enemy).GetEnemyData().IsBoss)
            {
                FieldManager fieldManager = ManagersMaster.Instance.FieldM;
                fieldManager.CurrentField.BGM.Play();
            }

            // プレイヤー経験値獲得
            int experience = Enemy.GetExperiece();
            Player.GainExeperience(experience);

            // 冒険パート管理者の戦闘フェイズを終了させ、次のフェイズに移行させる
            ManagersMaster.Instance.AdventureM.ChangeIntoNextPhase();
        }

        /// <summary>
        /// プレイヤーが敗北した際に実行するコルーチン
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnPlayerHasDefeated()
        {
            // プレイヤー敗北フラグON
            GameController.Instance.PlayerHasDefeated = true;

            // BGM継続フラグOFF
            GameController.Instance.ContinueBGM = false;

            // リザルトシーンへ移行する
            SceneChangeManager sceneChangeManager = ManagersMaster.Instance.SceneChangeM;
            yield return sceneChangeManager.ChangeScene(Glossary.Scene.Result);
        }

        /// <summary>
        /// 戦闘終了時に実行する共通処理のコルーチン
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnBattleEnd()
        {
            // UIコルーチン
            UIManager uiManager = ManagersMaster.Instance.UIM;
            yield return uiManager.OnStartBattleEnd();

            // プレイヤーのアイテムを未選択にする
            PlayerBattle.ChangeItemInUse(null);
        }


        protected override void UpdateInner()
        {
            switch (CurrentPhase)
            {
                case Phase.PlayerItemSelect:
                    UpdateOnPlayerItemSelectPhase();
                    break;
            }
        }

        /// <summary>
        /// プレイヤーのアイテム選択フェイズ時の更新処理
        /// </summary>
        private void UpdateOnPlayerItemSelectPhase()
        {
            InputManager input = ManagersMaster.Instance.InputM;

            // アイテムを選択していない場合
            if (!PlayerBattle.SelectedItemIndex.HasValue)
            {
                return;
            }

            // 決定ボタンが押されたか
            if (input.GetSubmitButtonDown())
            {
                // 選択中のアイテムは使用可能か
                if (PlayerBattle.GetSelectedItem().CanUse())
                {
                    // フェイズ移行
                    ChangePhase(Phase.PlayerStartCharging);
                }
            }
            // 軸が入力されたか
            else if (input.AxisIsInput())
            {
                // アイテム変更
                Vector2 axis = input.Get4DirectionsDown();
                if (axis == Vector2.left)
                {
                    ManagersMaster.Instance.AudioAssistantsM.FocusChange.Play();
                    PlayerBattle.ChangeItemInUseToPreviousIndex();
                }
                else if (axis == Vector2.right)
                {
                    ManagersMaster.Instance.AudioAssistantsM.FocusChange.Play();
                    PlayerBattle.ChangeItemInUseToNextIndex();
                }
            }
        }
    }
}
