using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// プレゼント箱の生成装置
    /// </summary>
    public class GiftBoxGenerator : ObjectOnFieldGenerator 
    {
        /// <summary>
        /// プレゼント箱のID
        /// </summary>
        [field: SerializeField]
        public int ID { get; private set; } = 1;

        public override void Generate()
        {
            base.Generate();

            GiftBox giftBox = ObjectInstance as GiftBox;
            giftBox.SetID(ID);
        }
    }
}
