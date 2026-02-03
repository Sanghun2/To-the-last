using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftListSD", menuName = "Scriptable Objects/CraftListSD")]
public class CraftListSD : SDBase
{
    public IReadOnlyList<CraftSD> craftLists => craftSDs;

    [SerializeField] CraftSD[] craftSDs;
}
