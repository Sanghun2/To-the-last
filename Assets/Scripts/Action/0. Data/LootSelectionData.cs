using System.Collections.Generic;
using UnityEngine;

public class LootSelectionData : SelectionDataBase
{
    public IReadOnlyList<LootInfo> AvailableItemList => lootItemDataList;

    public int DefaultLootCount => 1;

    private IReadOnlyList<LootInfo> lootItemDataList;

    public LootSelectionData(
        int requireMinutes, 
        string displayText, 
        Define.RequirementType requirementType, 
        Ingredient requirement,
        IReadOnlyList<LootInfo> lootInfos) 
        : base(requireMinutes, displayText, requirementType, requirement) {

        this.lootItemDataList = lootInfos;
    }
}
