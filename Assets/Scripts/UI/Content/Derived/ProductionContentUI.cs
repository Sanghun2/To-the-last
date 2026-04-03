using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class ProductionContentUI : ContentUIBase<ProductionContentSD>
{
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
}
