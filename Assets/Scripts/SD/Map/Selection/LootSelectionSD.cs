using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "LootSelectionSD", menuName = "Scriptable Objects/Selection/LootSelectionSD")]
public class LootSelectionSD : SelectionSD
{
    public IReadOnlyList<LootData> LootItemDataList => lootList;
    public float DefaultLootCount => defaultLootCount;

    [Space]
    [SerializeField] float defaultLootCount = 10;
    [SerializeField] List<LootData> lootList;
}

[Serializable]
public class LootData
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

public class LootSelectionContext : SelectionContext
{
    public float LootCountMutiflier => lootCountMultiflier;
    public InventoryBase TargetInventory => targetInventory;

    [SerializeField] float lootCountMultiflier = 1;
    [SerializeField] InventoryBase targetInventory;

    public LootSelectionContext(InventoryBase targetInventory) {
        this.targetInventory = targetInventory;
    }

    public LootSelectionContext SetLootCountMultiflier(float value) {
        lootCountMultiflier = value;
        return this;
    }
}