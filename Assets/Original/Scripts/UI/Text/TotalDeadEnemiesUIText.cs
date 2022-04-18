using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 死亡している敵の総数表示UIテキスト
    /// </summary>
    public class TotalDeadEnemiesUIText : UITextBase
    {
        /// <summary>
        /// 表示すべき死亡している敵の総数
        /// </summary>
        public int TotalDeadEnemiesToBeDisplayed { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            bool result = false;

            int totalDeadEnemies = GameController.Instance.TotalDeadEnemies;
            if (totalDeadEnemies != TotalDeadEnemiesToBeDisplayed)
            {
                TotalDeadEnemiesToBeDisplayed = totalDeadEnemies;

                StringTobeDisplayed = TotalDeadEnemiesToBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
