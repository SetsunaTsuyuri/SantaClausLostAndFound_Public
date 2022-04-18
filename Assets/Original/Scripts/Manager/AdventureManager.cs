using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

namespace DungeonRPG3D
{
    /// <summary>
    /// 冒険パートの管理者
    /// </summary>
    public class AdventureManager : ManagerBase
    {
        /// <summary>
        /// フェイズ
        /// </summary>
        public enum Phase
        {
            None,
            Start,
            EnemyPosition,
            Player,
            EnemyMove,
            Waiting,
            Battle,
            End,
            Goal
        }

        /// <summary>
        /// 現在のフェイズ
        /// </summary>
        public Phase CurrentPhase { get; private set; } = Phase.None;

        public override void Init()
        {
            base.Init();

            ChangePhase(Phase.Start);
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
            if (CurrentPhase == Phase.End ||
                CurrentPhase == Phase.Goal)
            {
                ChangePhase(Phase.EnemyPosition);
            }
            else
            {
                ChangePhase(CurrentPhase + 1);
            }
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

                case Phase.EnemyPosition:
                    OnStartEnemyPositionPhase();
                    break;

                case Phase.EnemyMove:
                    OnStartEnemyMovePhase();
                    break;

                case Phase.Battle:
                    OnStartBattlePhase();
                    break;

                case Phase.End:
                    OnStartEndPhase();
                    break;

                case Phase.Goal:
                    OnStartGoalPhase();
                    break;
            }
        }

        /// <summary>
        /// スタートフェイズ開始時の処理
        /// </summary>
        private void OnStartStartPhase()
        {
            // フェードイン+フェイズ移行
            StartCoroutine(FadeInAndChangeIntoNextPhase());
        }

        /// <summary>
        /// フェードイン処理の後、次のフェイズへ移行する
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeInAndChangeIntoNextPhase()
        {
            yield return ManagersMaster.Instance.UIM.DoFadeIn();
            ChangeIntoNextPhase();
        }

        /// <summary>
        /// 敵位置決定フェイズ開始時の処理
        /// </summary>
        private void OnStartEnemyPositionPhase()
        {
            // 敵配列取得
            Enemy[] enemies = ManagersMaster.Instance.ObjectsOnFieldM.Enemies;
            foreach (var enemy in enemies)
            {
                // 死亡しているならコンティニュー
                if (enemy.CurrentState == Creature.State.Dead)
                {
                    continue;
                }

                // 敵(移動取得)
                EnemyMove enemyMove = enemy.SelfMove as EnemyMove;

                // 移動方向決定
                enemyMove.SetDirectionToTheNextPosition(enemyMove.AI.SelectMoveDirection());

                // 移動しないならコンティニュー
                if (enemyMove.DirectionToTheNextPosition == Vector3.zero)
                {
                    continue;
                }

                // 移動先座標
                Vector2Int destination = enemyMove.ToNewPosition(enemyMove.DirectionToTheNextPosition);

                // 移動先決定
                enemy.SetNextPosition(destination);
            }

            // 敵の移動先表示パネル配置
            ManagersMaster.Instance.EffectM.DeployEnemyNextPositionPanels();

            // 次のフェイズに移行する
            ChangeIntoNextPhase();
        }

        /// <summary>
        /// 敵移動フェイズ開始時の処理
        /// </summary>
        private void OnStartEnemyMovePhase()
        {
            Enemy[] enemies = ManagersMaster.Instance.ObjectsOnFieldM.Enemies;
            foreach (var enemy in enemies)
            {
                // 移動しない場合はコンティニューする
                if (enemy.CurrentState == Creature.State.Dead ||
                    enemy.SelfMove.DirectionToTheNextPosition == Vector3.zero)
                {
                    continue;
                }

                // 回転開始
                enemy.SelfMove.StartRotation();

                // 移動先に入れるか？
                if (enemy.SelfMove.IsTravelableCell(enemy.NextPosition) &&
                    !enemy.SelfMove.CheckOtherObjectsNextPosition(enemy.NextPosition))
                {
                    // 移動開始
                    enemy.SelfMove.StartMove();
                }
                else
                {
                    // そこにいるのは敵対勢力か？
                    if (enemy.SelfMove.CheckOpponent(enemy.NextPosition))
                    {
                        // 戦闘準備態勢に移行する
                        enemy.ChangeState(Creature.State.PreparingForBattle);
                    }

                    // 移動先リセット
                    enemy.ResetNextPosition();
                }
            }

            // 次のフェイズに移行する
            ChangeIntoNextPhase();
        }

        /// <summary>
        /// バトルフェイズ開始時の処理
        /// </summary>
        private void OnStartBattlePhase()
        {
            // 敵の中に戦闘準備態勢の敵が存在する場合
            Enemy[] enemies = ManagersMaster.Instance.ObjectsOnFieldM.Enemies;
            foreach (var enemy in enemies)
            {
                if (enemy.CurrentState == Creature.State.PreparingForBattle)
                {
                    // 戦闘を開始し、このメソッドを終了する
                    StartCoroutine(ManagersMaster.Instance.BattleM.BeginBattle(enemy));
                    return;
                }
            }

            // 次のフェイズへ移行する
            ChangeIntoNextPhase();
        }

        /// <summary>
        /// エンドフェイズ開始時の処理
        /// </summary>
        private void OnStartEndPhase()
        {
            ChangeIntoNextPhase();
        }

        /// <summary>
        /// ゴールフェイズ開始時の処理
        /// </summary>
        private void OnStartGoalPhase()
        {
            GameController.Instance.UpdateBeforeNextFieldLevel();

            int currentFieldLevel = GameController.Instance.CurrentFieldLevel;
            int maxFieldLevel = GameController.Instance.GetMaxFieldLevel();
            SceneChangeManager sceneChangeManager = ManagersMaster.Instance.SceneChangeM;

            // 最後のステージをクリアした場合
            if (currentFieldLevel >= maxFieldLevel)
            {
                // BGMを継続しない
                GameController.Instance.ContinueBGM = false;

                // リザルトシーンへ移行する
                StartCoroutine(sceneChangeManager.ChangeScene(Glossary.Scene.Result));
            }
            else
            {
                // 回復SE
                ManagersMaster.Instance.AudioAssistantsM.Healing.Play();

                // BGMを継続する
                GameController.Instance.ContinueBGM = true;

                // 次のレベルへ移行する
                GameController.Instance.AddFieldLevel();
                StartCoroutine(sceneChangeManager.ChangeScene(Glossary.Scene.Game));
            }
        }

        protected override void UpdateInner()
        {
            switch (CurrentPhase)
            {
                case Phase.Player:
                    UpdateOnPlayerPhase();
                    break;

                case Phase.Waiting:
                    UpdateOnWaitingPhase();
                    break;
            }
        }

        /// <summary>
        /// プレイヤーフェイズ時の更新処理
        /// </summary>
        private void UpdateOnPlayerPhase()
        {
            // プレイヤーが待機状態になるのを待つ
            if (!PlayerIsIdle())
            {
                return;
            }

            // 移動入力がされた場合
            if (ManagersMaster.Instance.InputM.AxisIsInput())
            {
                // プレイヤー取得
                Player player = ManagersMaster.Instance.ObjectsOnFieldM.Player;

                // 移動方向設定
                player.SelfMove.SetDirectionToTheNextPosition(ManagersMaster.Instance.InputM.Get4Directions());

                // プレイヤー回転開始
                player.SelfMove.StartRotation();

                // 移動先のX,Y座標を求める
                Vector2Int destination = player.SelfMove.ToNewPosition(player.SelfMove.DirectionToTheNextPosition);

                // そこをプレイヤーの移動先として決定する
                player.SetNextPosition(destination);

                // 移動先に入れるか？
                if (player.SelfMove.IsTravelableCell(player.NextPosition) &&
                    !player.SelfMove.CheckOtherObjects(player.NextPosition))
                {
                    // 移動開始
                    player.SelfMove.StartMove();

                    // 次のフェイズへ移行する
                    ChangeIntoNextPhase();
                }
                else
                {
                    // そこにあるのはゴールか？
                    if (player.SelfMove.CheckGoal(destination))
                    {
                        // ゴールフェイズへ移行する
                        ChangePhase(Phase.Goal);
                    }

                    // そこにあるのはプレゼント箱か？(その場合触れた箱が開封される)
                    if (player.SelfMove.CheckGiftBox(destination))
                    {

                    }

                    // 入れない場合、そこにいるのは敵対勢力か？(その場合、触れた敵が戦闘準備態勢に移行する)
                    if (player.SelfMove.CheckOpponent(destination))
                    {
                        // バトルフェイズへ移行する
                        ChangePhase(Phase.Battle);
                    }

                    // 移動先リセット
                    player.ResetNextPosition();
                }
            }
        }

        /// <summary>
        /// 待機フェイズ時の更新処理
        /// </summary>
        private void UpdateOnWaitingPhase()
        {
            if (PlayerIsIdle())
            {
                ChangeIntoNextPhase();
            }
        }

        /// <summary>
        /// プレイヤーが待機状態か
        /// </summary>
        /// <returns></returns>
        private bool PlayerIsIdle()
        {
            Player player = ManagersMaster.Instance.ObjectsOnFieldM.Player;
            return player.CurrentState == Creature.State.Idle;
        }
    }
}
