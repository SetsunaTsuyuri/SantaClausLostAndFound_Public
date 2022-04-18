using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 牙
    /// </summary>
    public class Fang : OffensivePhysicalItem
    {
        public override ItemData GetData()
        {
            return ManagersMaster.Instance.ItemM.OffensivePhysicalItemDataList.FangData[ID];
        }

        public override ActionData GetActionData()
        {
            OffensivePhysicalActionDataList.ID id = (GetData() as OffensivePhysicalItemData).ActionID;
            return ManagersMaster.Instance.ActionDataM.OffensivePhysicalActionDataList.FangActionData[(int)id];
        }
    }
}
