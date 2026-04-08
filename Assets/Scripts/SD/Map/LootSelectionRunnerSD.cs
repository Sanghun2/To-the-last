using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootSelectionRunnerSD", menuName = "Scriptable Objects/Selection/Runner/LootSelectionRunnerSD")]
public class LootSelectionRunnerSD : SelectionRunnerSDBase, IReward
{
    public IReadOnlyList<DropInfo> Rewards => rewards;

    [SerializeField] List<DropInfo> rewards;
}

[Serializable]
public class DropInfo
{
    public int MinDropAmount => minDropAmount;
    public int MaxDropAmount => maxDropAmount;
    public ItemSD DropItem => dropItem;
    public int Weight => weight;    

    [SerializeField] ItemSD dropItem;
    [SerializeField] int minDropAmount;
    [SerializeField] int maxDropAmount;
    [SerializeField] int weight;
}
