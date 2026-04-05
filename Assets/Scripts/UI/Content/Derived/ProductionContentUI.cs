using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public class ProductionContentUI : ContentUIBase<ProductionContentSD>
{
    public Structure Structure => structure;

    [SerializeField] protected ItemInfoButton itemInfoButton;
    [SerializeField] protected RequirementUIContainer requirementUIContainer;
    private Structure structure;

    public override void InitContent(ProductionContentSD contentSD) {
        base.InitContent(contentSD);

        var requirements = contentSD.Requirements;
        SetRequirements(requirements);
        SetItemInfo(contentSD.Outputs[0].ItemSD.ID);
    }
    public void SetStructure(Structure structure) {
        this.structure = structure;

        var context = structure.StructureContext;
        if (context != null) {
            var currentState = context.ProcessState;
            UpdateExecutionButton(currentState, currentState);
            context.OnProcessStateChanged -= UpdateExecutionButton;
            context.OnProcessStateChanged += UpdateExecutionButton;
        }
    }
    public void UpdateExecutionButton(Structure.ProcessState currentState, Structure.ProcessState prevState) {
        bool interactable = currentState == Structure.ProcessState.Available;
        executionButton.SetInteractable(interactable);
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

    protected override void OnProgressStart() {
        var context = structure.StructureContext;
        if (context != null) {
            context.ProcessState = Structure.ProcessState.Processing;
        }
    }
    protected override void OnProgressComplete() {
        var context = structure.StructureContext;
        if (context != null) {
            context.ProcessState = Structure.ProcessState.Available;
        }

        if (Managers.Craft.TryCraftProduction(contentSD, this)) {
            
        }
        else {
            Debug.LogError($"<color=red>({contentSD.GetType()}) craft production failed</color>");
        }
    }

    private void OnEnable() {
        var context = structure.StructureContext;
        if (context != null) {
            context.OnProcessStateChanged -= UpdateExecutionButton;
            context.OnProcessStateChanged += UpdateExecutionButton;
        }
    }

    private void OnDisable() {
        var context = structure.StructureContext;
        if (context != null) {
            context.OnProcessStateChanged -= UpdateExecutionButton;
        }
    }
}
