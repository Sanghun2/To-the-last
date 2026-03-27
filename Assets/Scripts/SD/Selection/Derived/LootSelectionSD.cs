using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "LootSelectionSD", menuName = "Scriptable Objects/Selection/LootSelectionSD")]
public class LootSelectionSD : SelectionSD
{
    public IReadOnlyList<LootInfo> LootItemDataList => lootList;
    public float DefaultLootCount => defaultLootCount;

    [Space]
    [SerializeField] float defaultLootCount = 10;
    [SerializeField] List<LootInfo> lootList;
}

[Serializable]
public class LootInfo
{
    public ItemSD ItemSD => itemSD;
    public int MinAppearence => minAppearence;
    public int MaxAppearence => maxAppearence;
    public int Weight => weight;

    [SerializeField] ItemSD itemSD;
    [SerializeField] int weight = 1;
    [SerializeField] int minAppearence;
    [SerializeField] int maxAppearence;
}