using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// ファイルパス関連の便利クラス
    /// </summary>
    public class PathUtility
    {
        /// <summary>
        /// バックスラッシュをスラッシュに置換する
        /// </summary>
        /// <param name="text">置換したい文章</param>
        /// <returns></returns>
        public static string ReplaceBackSlashWithSlash(string text)
        {
            return text.Replace('\\', '/');
        }
    }
}
