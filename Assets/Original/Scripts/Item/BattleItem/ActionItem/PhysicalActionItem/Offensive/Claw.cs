using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 爪
    /// </summary>
    public class Claw : OffensivePhysicalItem
    {
        public override ItemData GetData()
        {
            return ManagersMaster.Instance.ItemM.OffensivePhysicalItemDataList.ClawData[ID];
        }

        public override ActionData GetActionData()
        {
            OffensivePhysicalActionDataList.ID id = (GetData() as OffensivePhysicalItemData).ActionID;
            return ManagersMaster.Instance.ActionDataM.OffensivePhysicalActionDataList.ClawActionData[(int)id];
        }
    }
}
