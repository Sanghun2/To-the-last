using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProductionContentUI : ContentUIBase<ProductionContentSD>
{
    public Structure Structure => structure;
    public override bool IsLocked => structure == null || contentSD.RequiredLevel > structure.StructureLevel;


    [SerializeField] protected ItemInfoButton itemInfoButton;
    [SerializeField] protected RequirementUIContainer requirementUIContainer;

    public override void InitContent(ProductionContentSD contentSD) {
        base.InitContent(contentSD);

        var requirements = contentSD.Requirements;
        SetRequirements(requirements);
        SetItemInfo(contentSD.Outputs[0].ItemSD.ID);
    }

    private void SetItemInfo(string itemID) {
        itemInfoButton.SetData(itemID);
    }

    private void SetRequirements(IReadOnlyList<Ingredient> requirements) {
        requirementUIContainer.ShowRequirements(requirements);
        requirementUIContainer.gameObject.SetActive(requirements != null);
    }

    protected override bool CanExecute() {
        if (!Managers.Inventory.TryGetInventoryByTag(out var inventories, Define.Tag.PLAYER, Define.Tag.STORAGE)) { return false; }
        if (!InventoryUtility.TryConsumeIngredients(inventories, contentSD.Requirements)) { return false; }

        return true;
    }

    protected override void OnProgressComplete() {
        base.OnProgressComplete();

        if (Managers.Craft.TryCraftProduction(contentSD, this)) {
            
        }
        else {
            Debug.LogError($"<color=red>({contentSD.GetType()}) craft production failed</color>");
        }
    }
}
