using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// 生物ステータス表示UI(画像が切り替わるタイプ)
    /// </summary>
    public abstract class CreatureStatusUIImageChangeBase : CreatureStatusUIImageBase
    {
        /// <summary>
        /// 表示すべき画像
        /// </summary>
        public Image ImageToBeDisplayed { get; protected set; } = null;

        protected override void UpdateDisplay()
        {
            SelfImage = ImageToBeDisplayed;
        }

    }

}
