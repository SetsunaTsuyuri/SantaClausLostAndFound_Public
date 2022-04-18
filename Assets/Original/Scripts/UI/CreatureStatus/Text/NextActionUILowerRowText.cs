using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 行動表示UIテキスト(上の行)
    /// </summary>
    public class NextActionUILowerRowText : CreatureStatusUITextBase
    {
        /// <summary>
        /// 威力と守備力の桁数
        /// </summary>
        public static readonly int PowerAndDefenseDigits = 3;

        /// <summary>
        /// チャージタイムの桁数
        /// </summary>
        public static readonly int ChargeTimeDigits = 3;

        /// <summary>
        /// 表示すべき攻撃力
        /// </summary>
        public int OffensivePowerToBeDisplayed { get; private set; } = -1;

        /// <summary>
        /// 表示すべき守備力
        /// </summary>
        public int DefensivePowerToBeDisplayed { get; private set; } = -1;

        /// <summary>
        /// 表示すべき魔力
        /// </summary>
        public int MagicalPowerToBeDisplayed { get; private set; } = -1;

        /// <summary>
        /// 表示すべきチャージタイム
        /// </summary>
        public int ChargeTimeToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            CreatureBattle creatureBattle = GetCreatureBattle();
            if (creatureBattle == null)
            {
                return false;
            }

            bool result = false;

            CreatureStatus status = creatureBattle.CurrentStatus;
            if (status.OffensivePower != OffensivePowerToBeDisplayed ||
                status.DefensivePower != DefensivePowerToBeDisplayed ||
                status.MagicalPower != MagicalPowerToBeDisplayed ||
                creatureBattle.GetChargeTimeToBeDisplayed() != ChargeTimeToBeDisplayed)
            {
                OffensivePowerToBeDisplayed = status.OffensivePower;
                DefensivePowerToBeDisplayed = status.DefensivePower;
                MagicalPowerToBeDisplayed = status.MagicalPower;
                ChargeTimeToBeDisplayed = creatureBattle.GetChargeTimeToBeDisplayed();

                // 表示テキスト
                string text = "";

                ActionData actionData = creatureBattle.NextActionData;
                if (actionData != null)
                {
                    switch (actionData.ImpactOnHp)
                    {
                        case ActionData.ImpactOnHpType.None:
                            break;

                        case ActionData.ImpactOnHpType.Damage:
                            text += $"{Glossary.Status.Power} ";
                            break;

                        case ActionData.ImpactOnHpType.Heal:
                            text += $"{Glossary.Status.Recovery} ";
                            break;
                    }

                    if (actionData.Calculation == ActionData.CalculationType.TrueDamage)
                    {
                        actionData.Power.ToString().PadLeft(PowerAndDefenseDigits);
                    }
                }

                if (creatureBattle.SelectedItemIndex.HasValue)
                {
                    int index = (int)creatureBattle.SelectedItemIndex;
                    BattleItem item = creatureBattle.EquippedItems[index];

                    if (item is OffensivePhysicalItem)
                    {
                        text += OffensivePowerToBeDisplayed.ToString().PadLeft(PowerAndDefenseDigits);
                    }
                    else if (item is MagicalItem)
                    {
                        text += MagicalPowerToBeDisplayed.ToString().PadLeft(PowerAndDefenseDigits);
                    }
                }

                text += $" {Glossary.Status.DefensivePower} " +
                    DefensivePowerToBeDisplayed.ToString().PadLeft(PowerAndDefenseDigits);

                text += $" {Glossary.Status.ChargeTime} " +
                    ChargeTimeToBeDisplayed.ToString().PadLeft(ChargeTimeDigits);

                TextToBeDisplayed = text;

                result = true;
            }

            return result;
        }
    }
}
