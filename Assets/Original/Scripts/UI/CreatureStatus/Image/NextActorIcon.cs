using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// 次の行動者アイコン
    /// </summary>
    public class NextActorIcon : UIDisplayBase
    {
        /// <summary>
        /// プレイヤーのアイコン
        /// </summary>
        [field: SerializeField]
        public Image PlayerIcon { get; private set; } = null;
        
        /// <summary>
        /// 敵のアイコン
        /// </summary>
        [field: SerializeField]
        public Image EnemyIcon { get; private set; } = null;

        /// <summary>
        /// プレイヤーを表示中か
        /// </summary>
        public bool IsPlayer { get; private set; } = false;

        protected override bool CheckValueHasChanged()
        {
            BattleManager battleManager = ManagersMaster.Instance.BattleM;
            if (battleManager.CurrentPhase == BattleManager.Phase.None)
            {
                return false;
            }

            bool result = false;


            CreatureBattle playerBattle = battleManager.PlayerBattle;
            int playerChargeTime = playerBattle.GetChargeTimeToBeDisplayed();

            CreatureBattle enemyBattle = battleManager.EnemyBattle;
            int enemyChargeTime = enemyBattle.GetChargeTimeToBeDisplayed();

            if (IsPlayer)
            {
                if (enemyChargeTime < playerChargeTime)
                {
                    result = true;
                }
            }
            else if (playerChargeTime <= enemyChargeTime)
            {
                result = true;
            }

            return result;
        }

        protected override void UpdateDisplay()
        {
            Switch();
        }

        /// <summary>
        /// アイコンの表示を切り替える
        /// </summary>
        private void Switch()
        {
            PlayerIcon.enabled = !PlayerIcon.enabled;
            EnemyIcon.enabled = !EnemyIcon.enabled;
            IsPlayer = !IsPlayer;
        }
    }
}
