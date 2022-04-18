using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// フィールド上の敵の最大数
    /// </summary>
    public class MaxEnemiesUIText : UITextBase
    {
        /// <summary>
        /// 敵の最大数
        /// </summary>
        public int MaxEnemiesToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            int maxEnemies = ManagersMaster.Instance.ObjectsOnFieldM.CountEnemies();
            if (MaxEnemiesToBeDisplayed != maxEnemies)
            {
                MaxEnemiesToBeDisplayed = maxEnemies;

                StringTobeDisplayed = MaxEnemiesToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
