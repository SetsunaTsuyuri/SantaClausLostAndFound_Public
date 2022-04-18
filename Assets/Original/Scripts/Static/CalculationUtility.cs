using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 計算関連の便利クラス
    /// </summary>
    public static class CalculationUtility
    {
        /// <summary>
        /// 割合を求める
        /// </summary>
        /// <param name="a">分子</param>
        /// <param name="b">分母</param>
        /// <returns>割合</returns>
        public static float GetPercentage(int a, int b)
        {
            float aFloat = a;
            float bFloat = b;

            return GetPercentage(aFloat, bFloat);
        }

        /// <summary>
        /// 割合を求める
        /// </summary>
        /// <param name="a">分子</param>
        /// <param name="b">分母</param>
        /// <returns>割合</returns>
        public static float GetPercentage(float a, float b)
        {
            if (b == 0.0f)
            {
                return 0.0f;
            }

            return a / b;
        }

        /// <summary>
        /// 経験値からレベルを求める
        /// </summary>
        /// <param name="experience">経験値</param>
        /// <returns>レベル</returns>
        public static int ToLevel(int experience)
        {
            int level = CreatureBattle.MinLevel;

            PlayerSetting setting = ManagersMaster.Instance.ObjectsOnFieldM.PlayerSetting;
            int nextLevelExp = setting.ExperienceRequiredToLevelUp;

            while (true)
            {
                experience -= nextLevelExp;
                if (experience < 0)
                {
                    break;
                }
                level++;
                nextLevelExp = AddNextLevelExp(nextLevelExp);
            }

            return level;
        }

        /// <summary>
        /// レベルからそれに達するために必要な経験値を求める
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int ToExperience(int level)
        {
            int experience = 0;

            PlayerSetting setting = ManagersMaster.Instance.ObjectsOnFieldM.PlayerSetting;
            int nextLevelExp = setting.ExperienceRequiredToLevelUp;

            for (int i = CreatureBattle.MinLevel; i < level; i++)
            {
                experience += nextLevelExp;
                nextLevelExp = AddNextLevelExp(nextLevelExp);
            }

            return experience;
        }

        /// <summary>
        /// 次のレベルアップに必要な経験値を増やす
        /// </summary>
        /// <param name="nextLevelExp">次のレベルアップに必要な経験値</param>
        /// <returns></returns>
        private static int AddNextLevelExp(int nextLevelExp)
        {
            PlayerSetting setting = ManagersMaster.Instance.ObjectsOnFieldM.PlayerSetting;
            float expCoefficient = setting.ExperienceCoefficient;

            return Mathf.FloorToInt(nextLevelExp * expCoefficient);
        }

        /// <summary>
        /// 次のレベルアップに必要な経験値を計算する
        /// </summary>
        /// <returns></returns>
        public static int CalculateExperienceRequiredToNextLevel(int experience)
        {
            int result = 0;

            int nextLevel = ToLevel(experience) + 1;

            if (nextLevel <= CreatureBattle.MaxLevel)
            {
                int nextLevelExp = ToExperience(nextLevel);

                result = nextLevelExp - experience;
            }

            return result;
        }
    }
}
