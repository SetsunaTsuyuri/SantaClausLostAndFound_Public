using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 次のレベルアップに必要な経験値の表示UIテキスト
    /// </summary>
    public class NextLevelExpUIText : UITextBase
    {
        /// <summary>
        /// プレイヤーの経験値
        /// </summary>
        public int PlayerExperience { get; private set; } = -1;

        protected override bool CheckValueHasChanged()
        {
            Player player = ManagersMaster.Instance.ObjectsOnFieldM.Player;
            if (player == null)
            {
                return false;
            }

            bool result = false;

            int experience = player.Experience;

            if (PlayerExperience != experience)
            {
                PlayerExperience = experience;

                int toBeDisplayed = CalculationUtility.CalculateExperienceRequiredToNextLevel(experience);

                StringTobeDisplayed = toBeDisplayed.ToString();

                result = true;
            }

            return result;
        }
    }
}
