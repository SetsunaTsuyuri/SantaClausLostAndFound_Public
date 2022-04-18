using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DungeonRPG3D
{
    /// <summary>
    /// 槍
    /// </summary>
    public class Spear : OffensivePhysicalItem
    {
        public override ItemData GetData()
        {
            return ManagersMaster.Instance.ItemM.OffensivePhysicalItemDataList.SpearData[ID];
        }

        public override ActionData GetActionData()
        {
            OffensivePhysicalActionDataList.ID id = (GetData() as OffensivePhysicalItemData).ActionID;
            return ManagersMaster.Instance.ActionDataM.OffensivePhysicalActionDataList.SpearActionData[(int)id];
        }
    }
}
