using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DungeonRPG3D
{
    /// <summary>
    /// エフェクトの管理者
    /// </summary>
    public class EffectManager : ManagerBase
    {
        /// <summary>
        /// 敵の移動先表示パネル
        /// </summary>
        [field: SerializeField]
        public NextPositionPanel EnemyNextPositionPanel { get; private set; } = null;

        /// <summary>
        /// 敵の移動先表示パネルのリスト
        /// </summary>
        public List<NextPositionPanel> EnemyNextPositionPanelList { get; private set; } = new List<NextPositionPanel>();

        /// <summary>
        /// ダメージ・回復演出の待機時間
        /// </summary>
        [field: SerializeField]
        public float WaitingTimeForDamageOrHealing { get; private set; } = 0.5f;

        /// <summary>
        /// 敵の死亡演出の待機時間
        /// </summary>
        [field: SerializeField]
        public float WaitingTimeForDeath { get; private set; } = 1.0f;

        public override void Init()
        {
            base.Init();

            InitEnemyNextPositionPanels();
        }

        /// <summary>
        /// 敵の移動先表示パネルを初期化する
        /// </summary>
        private void InitEnemyNextPositionPanels()
        {
            Enemy[] enemies = ManagersMaster.Instance.ObjectsOnFieldM.Enemies;
            foreach (var enemy in enemies)
            {
                GameObject instance = Instantiate(EnemyNextPositionPanel.gameObject, transform);
                NextPositionPanel panel = instance.GetComponent<NextPositionPanel>();

                panel.Init(enemy);
                EnemyNextPositionPanelList.Add(panel);
            }
        }

        /// <summary>
        /// 敵の移動先表示パネルを配置する
        /// </summary>
        public void DeployEnemyNextPositionPanels()
        {
            Enemy[] enemies = ManagersMaster.Instance.ObjectsOnFieldM.Enemies;
            for (int i = 0; i < enemies.Length; i++)
            {
                Vector3 newPosition = VectorUtility.ToVector3(enemies[i].NextPosition);
                EnemyNextPositionPanelList[i].SelfTransform.position = newPosition;
            }
        }

        /// <summary>
        /// プレイヤーが経験値を得たときの演出を実行する
        /// </summary>
        /// <param name="experience">経験値</param>
        public void PlayGainingExperienceDirection(int experience)
        {
            Player player = ManagersMaster.Instance.ObjectsOnFieldM.Player;

            // 効果音再生
            AudioAssistantsManager audioManager = ManagersMaster.Instance.AudioAssistantsM;
            audioManager.ObtainingExperience.Play();

            // UI表示
            UIManager uiManager = ManagersMaster.Instance.UIM;
            uiManager.DisplayObtainedExperience(experience, player.SelfTransform.position + Vector3.up);
        }

        /// <summary>
        /// 生物がダメージを受けたときの演出
        /// </summary>
        /// <param name="creatureBattle">生物(戦闘)</param>
        /// <param name="damage">ダメージ</param>
        /// <returns></returns>
        public IEnumerator PlayDamageDirection(CreatureBattle creatureBattle, int damage)
        {
            UIManager uiManager = ManagersMaster.Instance.UIM;
            AudioAssistantsManager audioAssistantsManager = ManagersMaster.Instance.AudioAssistantsM;

            // ダメージ表示
            uiManager.DisplayDamage(creatureBattle, damage);

            if (creatureBattle is PlayerBattle)
            {
                if (damage > 0)
                {
                    // 効果音
                    audioAssistantsManager.PlayerDamage.Play();

                    // カメラシェイク
                    CameraManger cameraManger = ManagersMaster.Instance.CameraM;
                    cameraManger.GeneratePlayerDamageImpulse();
                }

                // 待機
                yield return new WaitForSeconds(WaitingTimeForDamageOrHealing);
            }
            else
            {
                // アニメーション
                CreatureAnimation creatureAnimation = creatureBattle.Owner.SelfAnimation;
                if (creatureBattle.IsDead())
                {
                    // 効果音
                    audioAssistantsManager.EnemyDamage.Play();

                    // 死亡アニメ
                    creatureAnimation.Play(CreatureAnimation.ID.Dead);

                    // 待機
                    yield return new WaitForSeconds(WaitingTimeForDeath);
                }
                else
                {
                    if (damage > 0)
                    {
                        // 効果音
                        audioAssistantsManager.EnemyDamage.Play();

                        // ダメージアニメ
                        creatureAnimation.Play(CreatureAnimation.ID.Damaged);

                    }

                    // 待機
                    yield return new WaitForSeconds(WaitingTimeForDamageOrHealing);
                }
            }

        }

        /// <summary>
        /// 生物が回復したときの演出
        /// </summary>
        /// <param name="creatureBattle">生物(戦闘)</param>
        /// <param name="value">回復量</param>
        /// <returns></returns>
        public IEnumerator PlayHealingDirection(CreatureBattle creatureBattle, int value)
        {
            UIManager uiManager = ManagersMaster.Instance.UIM;
            AudioAssistantsManager audioManager = ManagersMaster.Instance.AudioAssistantsM;

            // 効果音再生
            audioManager.Healing.Play();

            // 回復量表示
            uiManager.DisplayHealing(creatureBattle, value);

            yield return new WaitForSeconds(WaitingTimeForDamageOrHealing);
        }

        /// <summary>
        /// 生物が行動するときの演出
        /// </summary>
        /// <param name="creatureBattle">生物(戦闘)</param>
        /// <returns></returns>
        public IEnumerator PlayCreatureActionDirection(CreatureBattle creatureBattle)
        {
            // 効果音再生
            AudioAssistantsManager audioManager = ManagersMaster.Instance.AudioAssistantsM;
            if (creatureBattle is PlayerBattle)
            {
                audioManager.StartPlayerAction.Play();
            }
            else
            {
                audioManager.StartEnemyAction.Play();
            }

            // パネル点滅
            UIManager uiManager = ManagersMaster.Instance.UIM;
            yield return uiManager.FlashActionPanel(creatureBattle);
        }
    }
}
