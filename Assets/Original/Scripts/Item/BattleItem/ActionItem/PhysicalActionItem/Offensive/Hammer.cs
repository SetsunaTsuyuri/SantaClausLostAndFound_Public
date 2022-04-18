using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 槌
    /// </summary>
    public class Hammer : OffensivePhysicalItem
    {
        public override ItemData GetData()
        {
            return ManagersMaster.Instance.ItemM.OffensivePhysicalItemDataList.HammerData[ID];
        }

        public override ActionData GetActionData()
        {
            OffensivePhysicalActionDataList.ID id = (GetData() as OffensivePhysicalItemData).ActionID;
            return ManagersMaster.Instance.ActionDataM.OffensivePhysicalActionDataList.HammerActionData[(int)id];
        }
    }
}
