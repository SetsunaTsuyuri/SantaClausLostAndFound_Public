using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 魔法の杖
    /// </summary>
    public class MagicStaff : DefensiveMagicalItem
    {
        public override ItemData GetData()
        {
            return ManagersMaster.Instance.ItemM.DefensiveMagicalItemDataList.MagicStaffDataList[ID];
        }

        public override ActionData GetActionData()
        {
            DefensiveMagicalActionDataList.ID id = (GetData() as DefensiveMagicalItemData).ActionID;
            return ManagersMaster.Instance.ActionDataM.DefensiveMagicalActionDataList.StaffActionData[(int)id];
        }
    }
}
