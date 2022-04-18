using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 尻尾
    /// </summary>
    public class Tail : OffensivePhysicalItem
    {
        public override ItemData GetData()
        {
            return ManagersMaster.Instance.ItemM.OffensivePhysicalItemDataList.TailData[ID];
        }

        public override ActionData GetActionData()
        {
            OffensivePhysicalActionDataList.ID id = (GetData() as OffensivePhysicalItemData).ActionID;
            return ManagersMaster.Instance.ActionDataM.OffensivePhysicalActionDataList.TailActionData[(int)id];
        }
    }
}
