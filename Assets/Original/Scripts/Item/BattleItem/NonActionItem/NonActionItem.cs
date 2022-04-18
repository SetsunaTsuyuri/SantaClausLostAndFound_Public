using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 行動を伴わない戦闘アイテム
    /// </summary>
    public class NonActionItem : BattleItem
    {
        public override ItemData GetData()
        {
            return ManagersMaster.Instance.ItemM.NonActionItemDataList.Data[ID];
        }
    }
}
