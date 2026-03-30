using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UtilityStructureSD", menuName = "Scriptable Objects/Structure/UtilityStructureSD")]
public class UtilityStructureSD : StructureSD, IContentContext<UtilityContentSD>
{
    public IReadOnlyList<UtilityContentSD> ContentList => utilityContents;

    [SerializeField] UtilityContentSD[] utilityContents;
}