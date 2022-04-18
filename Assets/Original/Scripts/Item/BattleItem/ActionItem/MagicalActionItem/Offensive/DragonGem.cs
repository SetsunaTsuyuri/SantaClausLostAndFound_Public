using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 竜石
    /// </summary>
    public class DragonGem : OffensiveMagicalItem
    {
        public override ItemData GetData()
        {
            return ManagersMaster.Instance.ItemM.OffensiveMagicalItemDataList.DragonGemDataList[ID];
        }

        public override ActionData GetActionData()
        {
            OffensiveMagicalActionDataList.ID id = (GetData() as OffensiveMagicalItemData).ActionID;
            return ManagersMaster.Instance.ActionDataM.OffensiveMagicalActionDataList.DragonGemActionData[(int)id];
        }
    }
}
