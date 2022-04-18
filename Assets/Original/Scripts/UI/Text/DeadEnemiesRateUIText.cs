using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 敵の死亡率表示UIテキスト
    /// </summary>
    public class DeadEnemiesRateUIText : UITextBase
    {
        /// <summary>
        /// 表示すべき死亡率
        /// </summary>
        public float DeadEnemiesRateToBeDisplayed { get; private set; } = -1.0f;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            float deadEnemiesRate = GameController.Instance.DeadEnemiesRate;
            if (deadEnemiesRate != DeadEnemiesRateToBeDisplayed)
            {
                DeadEnemiesRateToBeDisplayed = deadEnemiesRate;

                string newText = Mathf.Floor(deadEnemiesRate * 100) + "%";

                StringTobeDisplayed = newText;

                result = true;
            }

            return result;
        }

    }

}
