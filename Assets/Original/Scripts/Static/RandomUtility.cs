using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 乱数関連の便利クラス
    /// </summary>
    public class RandomUtility
    {
        /// <summary>
        /// 0から最大値-1までの重複なし乱数配列を取得する
        /// </summary>
        /// <param name="max">最大値</param>
        /// <returns></returns>
        public static int[] GetRandomArray(int max)
        {
            int[] array = new int[max];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
            for (int i = 0; i < array.Length; i++)
            {
                int j = Random.Range(i, array.Length);
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            return array;
        }
    }

}
