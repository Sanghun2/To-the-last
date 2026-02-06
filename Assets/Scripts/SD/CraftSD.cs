using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftSD", menuName = "Scriptable Objects/CraftSD")]
public class CraftSD : SDBase
{
    public IReadOnlyList<CraftData> Requirements => requirements;

    [SerializeField] CraftData[] requirements;
}


[Serializable]
public class CraftData
{
    public ItemSD ItemSD => itemSD;
    public int NeedAmount => needAmount;

    [SerializeField] ItemSD itemSD;
    [SerializeField] int needAmount;
}