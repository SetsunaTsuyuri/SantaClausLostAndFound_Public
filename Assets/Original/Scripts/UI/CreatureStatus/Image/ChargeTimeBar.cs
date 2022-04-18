using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DungeonRPG3D
{
    /// <summary>
    /// チャージタイムバー
    /// </summary>
    public class ChargeTimeBar : CreatureStatusGaugeBase
    {
        /// <summary>
        /// 表示すべきチャージタイムの値
        /// </summary>
        public int ChargeTimeToBeDisplayed { get; private set; } = -1;

        /// <summary>
        /// 戦闘ステート
        /// </summary>
        public CreatureBattle.State BattleState { get; private set; } = CreatureBattle.State.Charging;

        public bool BattleStateHasChanged { get; private set; } = false;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            int chargeTime = creatureBattle.GetChargeTimeToBeDisplayed();
            CreatureBattle.State state = creatureBattle.CurrentState;
            if (chargeTime != ChargeTimeToBeDisplayed)
            {
                ChargeTimeToBeDisplayed = chargeTime;

                result = true;
            }

            return result;
        }

        protected override void UpdateDisplay()
        {
            float scale = CalculationUtility.GetPercentage(ChargeTimeToBeDisplayed, CreatureBattle.MaxChargeTimeBeforeAction);
            SelfAnimation.ChangeFillAmount(SelfImage, 1.0f - scale, IncreaseAndDecreaseDuration);
        }

        /// <summary>
        /// ゲージの量を初期化する
        /// </summary>
        public void InitFillAmount()
        {
            SelfImage.fillAmount = 0.0f;
        }
    }
}
