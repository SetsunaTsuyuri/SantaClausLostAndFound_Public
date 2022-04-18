using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 魔導書
    /// </summary>
    public class Grimoire : OffensiveMagicalItem
    {
        public override ItemData GetData()
        {
            return ManagersMaster.Instance.ItemM.OffensiveMagicalItemDataList.GrimoierDataList[ID];
        }

        public override ActionData GetActionData()
        {
            OffensiveMagicalActionDataList.ID id = (GetData() as OffensiveMagicalItemData).ActionID;
            return ManagersMaster.Instance.ActionDataM.OffensiveMagicalActionDataList.GrimoireActionData[(int)id];
        }
    }
}
