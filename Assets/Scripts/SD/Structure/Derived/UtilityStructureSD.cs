using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UtilityStructureSD", menuName = "Scriptable Objects/Structure/UtilityStructureSD")]
public class UtilityStructureSD : StructureSD
{
    public IReadOnlyList<UtilityContent> ContentList => utilityContents;

    [SerializeField] UtilityContent[] utilityContents;
}

[Serializable]
public class UtilityContent
{

}