using System;
using System.Collections.Generic;
using BillotGames;
using UnityEngine;

public sealed class SelectActionProcessorRegistry : TypeRegistry<SelectionSD, SelectActionProcessor>
{
    public SelectActionProcessorRegistry() {
        Register<LootSelectionSD>(new LootSelectActionProcessor());
    }

    public bool TryGenerateActionData(SelectionSD selectionSD, SelectActionContext context, out ActionData actionData) {
        actionData = null;

        if (TryGet(selectionSD, out var processor)) {
            return processor.TryGenerateAction(selectionSD, context, out actionData);
        }

        Debug.LogError($"<color=red>({selectionSD.GetType()}) processor null</color>");
        return false;
    }
}
