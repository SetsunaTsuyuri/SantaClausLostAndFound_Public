using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 敵の総数表示UIテキスト
    /// </summary>
    public class TotalEnemiesUIText : UITextBase
    {
        /// <summary>
        /// 表示すべき敵の総数
        /// </summary>
        public int TotalEnemiesToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            int totalEnemies = GameController.Instance.TotalEnemies;
            if (totalEnemies != TotalEnemiesToBeDisplayed)
            {
                TotalEnemiesToBeDisplayed = totalEnemies;

                StringTobeDisplayed = TotalEnemiesToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
