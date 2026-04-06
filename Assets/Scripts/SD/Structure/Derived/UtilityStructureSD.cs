using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UtilityStructureSD", menuName = "Scriptable Objects/Structure/UtilityStructureSD")]
public class UtilityStructureSD : StructureSDBase, IContentContext<ActivityContentSD>
{
    public IReadOnlyList<ActivityContentSD> ContentList => utilityContents;


    [SerializeField] ActivityContentSD[] utilityContents;
}