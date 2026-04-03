using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class ProductionContentUI : ContentUIBase<ProductionContentSD>
{
    [SerializeField] protected RequirementUIContainer requirementUIContainer;

    public override void InitContent(ProductionContentSD contentSD) {
        base.InitContent(contentSD);

        var requirements = contentSD.Requirements;
        SetRequirements(requirements);
    }

    private void SetRequirements(IReadOnlyList<Ingredient> requirements) {
        requirementUIContainer.ShowList(requirements);
        requirementUIContainer.gameObject.SetActive(requirements != null);
    }
}
