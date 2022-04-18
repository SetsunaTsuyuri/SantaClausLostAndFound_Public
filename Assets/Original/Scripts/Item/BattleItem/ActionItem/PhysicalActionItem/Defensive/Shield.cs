using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 盾
    /// </summary>
    public class Shield : DefensivePhysicalItem
    {
        public override ItemData GetData()
        {
            return ManagersMaster.Instance.ItemM.DefensivePhysicalItemDataList.ShieldData[ID];
        }

        public override ActionData GetActionData()
        {
            DefensivePhysicalActionDataList.ID id = (GetData() as DefensivePhysicalItemData).ActionID;
            return ManagersMaster.Instance.ActionDataM.DefensivePhysicalActionDataList.ShieldActionData[(int)id];
        }
    }
}
