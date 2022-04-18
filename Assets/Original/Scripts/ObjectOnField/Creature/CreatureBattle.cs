using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物の戦闘
    /// </summary>
    public abstract class CreatureBattle : CreatureSubComponent
    {
        /// <summary>
        /// 最小レベル
        /// </summary>
        public static readonly int MinLevel = 1;

        /// <summary>
        /// 最大レベル
        /// </summary>
        public static readonly int MaxLevel = 100;

        /// <summary>
        /// レベルによるステータス補正倍率
        /// </summary>
        public static readonly float LevelCorrectionMultiplier = 0.04f;

        /// <summary>
        /// 行動前の最小チャージタイム
        /// </summary>
        public static readonly int MinChargeTimeBeforeAction = 1;

        /// <summary>
        /// 行動前の最大チャージタイム
        /// </summary>
        public static readonly int MaxChargeTimeBeforeAction = 30;

        /// <summary>
        /// 対戦相手
        /// </summary>
        public CreatureBattle Opponent { get; protected set; } = null;

        /// <summary>
        /// ステート
        /// </summary>
        public enum State
        {
            Idle,
            Charging
        }

        /// <summary>
        /// 現在のステート
        /// </summary>
        public State CurrentState { get; protected set; } = State.Idle;

        /// <summary>
        /// 現在のレベル
        /// </summary>
        public int CurrentLevel { get; protected set; } = 1;

        /// <summary>
        /// 残りHP
        /// </summary>
        public int RemainingHp { get; protected set; } = 0;

        /// <summary>
        /// 残りチャージタイム
        /// </summary>
        public int RemainingChargeTime { get; protected set; } = 0;

        /// <summary>
        /// 最大チャージタイム
        /// </summary>
        public int MaxChargeTime { get; protected set; } = 0;

        /// <summary>
        /// 現在のステータス
        /// </summary>
        public CreatureStatus CurrentStatus { get; protected set; } = new CreatureStatus();

        /// <summary>
        /// 装備アイテムリスト
        /// </summary>
        public List<BattleItem> EquippedItems { get; protected set; } = new List<BattleItem>();

        /// <summary>
        /// 何番目のアイテムが選択されているか(nullなら何も選択されていない)
        /// </summary>
        public int? SelectedItemIndex { get; protected set; } = null;

        /// <summary>
        /// 次に実行する行動のデータ
        /// </summary>
        public ActionData NextActionData { get; protected set; } = null;

        /// <summary>
        /// 行動可能か
        /// </summary>
        /// <returns></returns>
        public bool CanAct()
        {
            return true;
        }

        /// <summary>
        /// 死亡しているか
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            return RemainingHp <= 0;
        }

        /// <summary>
        /// アイテム選択の準備ができているか
        /// </summary>
        /// <returns>チャージタイムゼロかつ待機状態ならtrue</returns>
        public bool IsReadyToSelectItem()
        {
            bool result = false;

            if (RemainingChargeTime == 0 &&
                CurrentState == State.Idle)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 行動実行の準備ができているか
        /// </summary>
        /// <returns>チャージタイムゼロかつチャージ状態ならtrue</returns>
        public bool IsReadyToExecuteAction()
        {
            bool result = false;

            if (RemainingChargeTime == 0 &&
                CurrentState == State.Charging)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// レベルを設定する
        /// </summary>
        /// <returns></returns>
        public void SetLevel(int level)
        {
            if (level < MinLevel)
            {
                level = MinLevel;
            }
            else if (level > MaxLevel)
            {
                level = MaxLevel;
            }
            CurrentLevel = level;
        }

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <returns>データ</returns>
        public CreatureData GetData()
        {
            return Owner.GetData();
        }

        /// <summary>
        /// 選択中のアイテムを取得する
        /// </summary>
        /// <returns></returns>
        public BattleItem GetSelectedItem()
        {
            return EquippedItems[(int)SelectedItemIndex];
        }

        /// <summary>
        /// UIで表示すべきチャージタイムを取得する
        /// </summary>
        /// <returns></returns>
        public int GetChargeTimeToBeDisplayed()
        {
            int value = 0;

            switch (CurrentState)
            {
                case State.Idle:
                    value = MaxChargeTime;
                    break;

                case State.Charging:
                    value = RemainingChargeTime;
                    break;
            }

            return value;
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        public virtual void Init()
        {
            // 装備アイテム初期化
            InitEquippedItems();

            // ステータス更新
            UpdateStatus();

            // HP初期化
            RemainingHp = CurrentStatus.MaxHp; ;
        }

        /// <summary>
        /// 対戦相手を設定する
        /// </summary>
        /// <param name="opponent">対戦相手</param>
        public void SetOpponent(CreatureBattle opponent)
        {
            Opponent = opponent;
        }

        /// <summary>
        /// 装備アイテムリストにデータの値を代入する
        /// </summary>
        public void SetEquippedItemsFromData()
        {
            EquippedItems = new List<BattleItem>();

            BattleItem[] battleItems = Owner.GetData().EquippedItems;
            foreach (var item in battleItems)
            {
                EquippedItems.Add(item);
            }
        }

        /// <summary>
        /// 装備アイテムを初期化する
        /// </summary>
        private void InitEquippedItems()
        {
            foreach (var item in EquippedItems)
            {
                item.Init();
            }
        }

        /// <summary>
        /// 使用アイテムを変更する
        /// </summary>
        public void ChangeItemInUse(int? newIndex)
        {
            // 番号変更
            SelectedItemIndex = newIndex;

            // ステータス更新
            UpdateStatus();

            // アクションデータ更新
            UpdateNextActionData();
        }

        /// <summary>
        /// 使用アイテムを次の番号に切り替える
        /// </summary>
        public void ChangeItemInUseToNextIndex()
        {
            if (!SelectedItemIndex.HasValue)
            {
                return;
            }

            int index = (int)SelectedItemIndex;
            index++;
            if (index >= EquippedItems.Count)
            {
                index = 0;
            }

            ChangeItemInUse(index);
        }

        /// <summary>
        /// 使用アイテムを前の番号に切り替える
        /// </summary>
        public void ChangeItemInUseToPreviousIndex()
        {
            if (!SelectedItemIndex.HasValue)
            {
                return;
            }

            int index = (int)SelectedItemIndex;
            index--;
            if (index < 0)
            {
                index = EquippedItems.Count - 1;
            }

            ChangeItemInUse(index);
        }

        /// <summary>
        /// ダメージを受ける(コルーチン)
        /// </summary>
        /// <param name="value">ダメージ</param>
        public virtual IEnumerator RecieveDamageCoroutine(int value)
        {
            // ダメージ
            int damageAmount = RecieveDamage(value);

            // 演出
            EffectManager effectManager = ManagersMaster.Instance.EffectM;
            yield return effectManager.PlayDamageDirection(this, damageAmount);
        }

        /// <summary>
        /// ダメージを受ける
        /// </summary>
        /// <param name="value">ダメージ</param>
        /// <returns>ダメージ</returns>
        public int RecieveDamage(int value)
        {
            if (value < 0)
            {
                value = 0;
            }

            RemainingHp -= value;

            if (RemainingHp < 0)
            {
                RemainingHp = 0;
            }

            // HPがゼロになった場合
            if (RemainingHp == 0)
            {
                // 本体を死亡状態にする
                Owner.ChangeState(Creature.State.Dead);
            }

            return value;
        }

        /// <summary>
        /// HPを回復する(コルーチン)
        /// </summary>
        /// <param name="value">回復量</param>
        public IEnumerator RecoverHpCoroutine(int value)
        {
            // 回復量
            int healingAmount = RecoverHp(value);

            // 演出
            EffectManager effectManager = ManagersMaster.Instance.EffectM;
            yield return effectManager.PlayHealingDirection(this, healingAmount);
        }

        /// <summary>
        /// HPを回復する
        /// </summary>
        /// <param name="value">回復量</param>
        /// <returns>回復量</returns>
        public int RecoverHp(int value)
        {
            if (value < 0)
            {
                value = 0;
            }

            RemainingHp += value;

            if (RemainingHp > CurrentStatus.MaxHp)
            {
                RemainingHp = CurrentStatus.MaxHp;
            }

            return value;
        }

        /// <summary>
        /// ステートを変更する
        /// </summary>
        /// <param name="newState">新しいステート</param>
        public void ChangeState(State newState)
        {
            CurrentState = newState;
        }

        /// <summary>
        /// チャージタイムを減らす
        /// </summary>
        public void DecreaseChargeTime(int value)
        {
            RemainingChargeTime -= value;

            if (RemainingChargeTime < 0)
            {
                RemainingChargeTime = 0;
            }
        }

        /// <summary>
        /// 最大チャージタイムを設定する
        /// </summary>
        private void SetMaxChargeTime()
        {
            // アイテムを選択していない場合
            if (!SelectedItemIndex.HasValue)
            {
                // 最小チャージタイムを設定して終了する
                MaxChargeTime = MinChargeTimeBeforeAction;
                return;
            }

            ActionItemData data = GetSelectedItem().GetData() as ActionItemData;
            MaxChargeTime = data.ChargeTime;

            MaxChargeTime -= CurrentStatus.Agility;

            if (MaxChargeTime < MinChargeTimeBeforeAction)
            {
                MaxChargeTime = MinChargeTimeBeforeAction;
            }
            else if (MaxChargeTime > MaxChargeTimeBeforeAction)
            {
                MaxChargeTime = MaxChargeTimeBeforeAction;
            }
        }

        /// <summary>
        /// 行動実行のためのチャージを開始する
        /// </summary>
        public IEnumerator StartCharging()
        {
            // チャージタイムセット
            RemainingChargeTime = MaxChargeTime;

            // クイックアクションなら即座に実行する
            if (NextActionData.IsQuick)
            {
                yield return ExecuteAction(NextActionData);
            }
            else
            {
                // チャージ状態に移行する
                ChangeState(State.Charging);
            }
        }

        /// <summary>
        /// 行動を実行する
        /// </summary>
        public virtual IEnumerator ExecuteAction()
        {
            yield return ExecuteAction(NextActionData);
        }

        /// <summary>
        /// 行動を実行する
        /// </summary>
        /// <param name="actionData">行動内容</param>
        public virtual IEnumerator ExecuteAction(ActionData actionData)
        {
            // 演出
            EffectManager effectManager = ManagersMaster.Instance.EffectM;
            yield return effectManager.PlayCreatureActionDirection(this);

            // 対象を設定する
            CreatureBattle target = null;
            switch (actionData.Target)
            {
                case ActionData.TargetType.Opponent:
                    target = Opponent;
                    break;

                case ActionData.TargetType.MySelf:
                    target = this;
                    break;
            }

            // 対象が存在する場合
            if (target != null)
            {
                // 行動回数分繰り返す
                for (int i = 0; i < actionData.NumberOfActions; i++)
                {
                    // HPに対する影響
                    switch (actionData.ImpactOnHp)
                    {
                        // なし
                        case ActionData.ImpactOnHpType.None:
                            break;

                        // ダメージ
                        case ActionData.ImpactOnHpType.Damage:
                            int damageamount = CalculateDamage(target, actionData);
                            yield return target.RecieveDamageCoroutine(damageamount);
                            break;

                        // 回復
                        case ActionData.ImpactOnHpType.Heal:
                            int healingAmount = CalculateHealing(actionData);
                            yield return target.RecoverHpCoroutine(healingAmount);
                            break;
                    }
                }
            }

            // 待機状態に移行する
            ChangeState(State.Idle);
        }

        /// <summary>
        /// ダメージ量を計算する
        /// </summary>
        /// <param name="target">対象</param>
        /// <param name="actionData">行動内容</param>
        /// <returns>ダメージ量</returns>
        private int CalculateDamage(CreatureBattle target, ActionData actionData)
        {
            int value = 0;

            switch (actionData.Calculation)
            {
                case ActionData.CalculationType.TrueDamage:
                    value += actionData.Power;
                    break;

                case ActionData.CalculationType.Physical:
                    value += CurrentStatus.OffensivePower - target.CurrentStatus.DefensivePower;
                    break;

                case ActionData.CalculationType.Magical:
                    value += CurrentStatus.MagicalPower;
                    break;
            }

            // 弱点属性補正
            if (CheckOponentsWeaknessAttribute(target, actionData))
            {
                BattleManager battleManager = ManagersMaster.Instance.BattleM;
                float multiplier = battleManager.BattleSetting.WeaknessAttributeMultiplier;
                value = Mathf.FloorToInt(value * multiplier);
            }

            return value;
        }

        /// <summary>
        /// 弱点属性か
        /// </summary>
        /// <param name="actionData">行動データ</param>
        /// <returns></returns>
        private bool CheckOponentsWeaknessAttribute(CreatureBattle target, ActionData actionData)
        {
            bool result = false;

            ActionData.AttributeType[] weaknessAttributes = target.GetData().WeaknessAttributes;
            foreach (var attribute in weaknessAttributes)
            {
                if (attribute == actionData.Attribute)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 回復量を計算する
        /// </summary>
        /// <returns>回復量</returns>
        private int CalculateHealing(ActionData actionData)
        {
            int value = 0;

            switch (actionData.Calculation)
            {
                case ActionData.CalculationType.TrueDamage:
                    value += actionData.Power;
                    break;

                case ActionData.CalculationType.Physical:
                    value += CurrentStatus.OffensivePower;
                    break;

                case ActionData.CalculationType.Magical:
                    value += CurrentStatus.MagicalPower;
                    break;
            }

            return value;
        }

        /// <summary>
        /// 次に実行する行動のデータを更新する
        /// </summary>
        public void UpdateNextActionData()
        {
            // データをnullにする
            NextActionData = null;

            // アイテムが選択されていない場合
            if (!SelectedItemIndex.HasValue)
            {
                //「何もしない」を次の行動とする
                NextActionData = GetNoActionData();
            }
            else
            {
                // アイテムの種類に対応する行動データを取得する
                NextActionData = GetSelectedItemActionData();
            }
        }

        /// <summary>
        /// 「何もしない」行動を取得する
        /// </summary>
        /// <returns>行動データ</returns>
        private ActionData GetNoActionData()
        {
            int id = (int)SpecialActionDataList.ID.NoAction;
            ActionData newActionAction = ManagersMaster.Instance.ActionDataM.SpecialActionDataList.Data[id];

            return newActionAction;
        }

        /// <summary>
        /// 選択しているアイテムの行動データを取得する
        /// </summary>
        /// <returns>行動データ</returns>
        private ActionData GetSelectedItemActionData()
        {
            // アイテムの種類に応じてアクションデータを取得する
            ActionData newActionData;

            BattleItem selectedItem = EquippedItems[(int)SelectedItemIndex];
            switch (selectedItem)
            {
                case Sword sword:
                    newActionData = sword.GetActionData();
                    break;

                case Spear spear:
                    newActionData = spear.GetActionData();
                    break;

                case Hammer hammer:
                    newActionData = hammer.GetActionData();
                    break;

                case Claw claw:
                    newActionData = claw.GetActionData();
                    break;

                case Fang fang:
                    newActionData = fang.GetActionData();
                    break;

                case Tail tail:
                    newActionData = tail.GetActionData();
                    break;

                case Shield shield:
                    newActionData = shield.GetActionData();
                    break;

                case Grimoire grimoire:
                    newActionData = grimoire.GetActionData();
                    break;

                case DragonGem dragonGem:
                    newActionData = dragonGem.GetActionData();
                    break;

                case MagicStaff magicStaff:
                    newActionData = magicStaff.GetActionData();
                    break;

                default:
                    newActionData = GetNoActionData();
                    break;
            }

            return newActionData;
        }

        /// <summary>
        /// ステータスを更新する
        /// </summary>
        public void UpdateStatus()
        {
            // データ代入
            SetStatusFromData();

            // レベル補正
            AddLevelCollection();

            // 非行動アイテム補正
            AddNonActionItemsCrrection();

            // 行動アイテム補正
            AddSelectedActionItemsCorrection();

            // 最大チャージタイム設定
            SetMaxChargeTime();
        }

        /// <summary>
        /// 現在ステータスにデータの値を代入する
        /// </summary>
        private void SetStatusFromData()
        {
            CurrentStatus = GetData().Status;
        }

        /// <summary>
        /// レベル補正を加算する
        /// </summary>
        private void AddLevelCollection()
        {
            CreatureStatus newStatus = CurrentStatus;

            float multiplier = (CurrentLevel - 1) * LevelCorrectionMultiplier;

            newStatus.MaxHp += Mathf.RoundToInt(newStatus.MaxHp * multiplier);
            newStatus.OffensivePower += Mathf.RoundToInt(newStatus.OffensivePower * multiplier);
            newStatus.DefensivePower += Mathf.RoundToInt(newStatus.DefensivePower * multiplier);
            newStatus.MagicalPower += Mathf.RoundToInt(newStatus.MagicalPower * multiplier);

            CurrentStatus = newStatus;

        }

        /// <summary>
        /// 非行動アイテムのステータス補正を加算する
        /// </summary>
        private void AddNonActionItemsCrrection()
        {
            CreatureStatus newStatus = CurrentStatus;

            foreach (var item in EquippedItems)
            {
                if (item is NonActionItem)
                {
                    NonActionItemData itemData = item.GetData() as NonActionItemData;

                    newStatus.OffensivePower += itemData.OffensivePowerCorrection;
                    newStatus.DefensivePower += itemData.DefensivePowerCorrection;
                    newStatus.MagicalPower += itemData.MagicalPowerCorrection;
                    newStatus.Agility += itemData.AgilityCorrection;
                }
            }

            CurrentStatus = newStatus;
        }

        /// <summary>
        /// 選択中の行動アイテムのステータス補正を加算する
        /// </summary>
        private void AddSelectedActionItemsCorrection()
        {
            // 選択していないなら中止する
            if (!SelectedItemIndex.HasValue)
            {
                return;
            }

            // 行動アイテムデータ取得
            if (!(GetSelectedItem().GetData() is ActionItemData itemData))
            {
                return;
            }

            // 現在ステータス取得
            CreatureStatus newStatus = CurrentStatus;

            // 物理アイテム(攻撃力・守備力)補正
            if (itemData is PhysicalItemData physicalItemData)
            {
                newStatus.OffensivePower += physicalItemData.OffensivePowerCorrection;
                newStatus.DefensivePower += physicalItemData.DefensivePowerCorrection;
            }
            // 魔法アイテム(魔力)補正
            else if (itemData is MagicalItemData magicalItemData)
            {
                newStatus.MagicalPower += magicalItemData.MagicalPowerCorrection;
            }

            // ステータス反映
            CurrentStatus = newStatus;
        }
    }
}
