using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UtilityStructureSD", menuName = "Scriptable Objects/Structure/UtilityStructureSD")]
public class UtilityStructureSD : StructureSD
{
    public override Type GetUIType() {
        return typeof(UtilityStructureUI);
    }
}
