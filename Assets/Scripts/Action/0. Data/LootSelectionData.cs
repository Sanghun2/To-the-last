using System.Collections.Generic;
using UnityEngine;

public class LootSelectionData : SelectionDataBase
{
    public IReadOnlyList<LootData> AvailableItemList => lootItemDataList;

    public int DefaultLootCount { get; internal set; }

    private IReadOnlyList<LootData> lootItemDataList;

    public LootSelectionData(IReadOnlyList<LootData> lootItemDataList) {
        this.lootItemDataList = lootItemDataList;
    }
}
