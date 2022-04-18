using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// タイトルシーンの管理者
    /// </summary>
    public class TitleSceneManager : SceneStateManagerBase
    {
        protected override IEnumerator OnStartStartPhase()
        {
            yield return base.OnStartStartPhase();

            // 次のフェイズへ
            ChangeIntoNextPhase();
        }
    }
}
