using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// フィールド上の死亡している敵の数
    /// </summary>
    public class DeadEnemiesUIText : UITextBase
    {
        /// <summary>
        /// 死亡している敵の数
        /// </summary>
        public int DeadEnemiesToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            int deadEnemies = ManagersMaster.Instance.ObjectsOnFieldM.CountDeadEnemies();
            if (DeadEnemiesToBeDisplayed != deadEnemies)
            {
                DeadEnemiesToBeDisplayed = deadEnemies;

                StringTobeDisplayed = DeadEnemiesToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
