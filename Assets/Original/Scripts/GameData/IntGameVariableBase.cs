using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    public class IntGameVariableBase : ScriptableObject
    {
        public IntVariable Value { get; protected set; } = new IntVariable(); 
    }
}
