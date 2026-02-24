using System;
using BilliotGames;
using UnityEngine;


public class LootSelectionActionContext : SelectionActionContext
{
    public InventoryBase Inventory => inventory;

    private InventoryBase inventory;

    public LootSelectionActionContext(SelectionSD selectionSD, InventoryBase inventory) : base(selectionSD, selectionSD.RequireMinutes) {
        this.inventory = inventory;
    }
}

public class LootSelectionActionFactory : SelectionActionFactory
{
    public override ActionData CreateAction(SelectionActionContext context) {
        var lootContext = (LootSelectionActionContext)context;
        return new ActionData(() => Loot(lootContext));
    }

    private void Loot(LootSelectionActionContext lootContext) {
        var targetInven = lootContext.Inventory;
        var lootSD = (LootSelectionSD)lootContext.SelectionSD;

        FocusJob job = new FocusJob(lootContext.JobDuration, onComplete: () => {
            var selectionContext = new LootSelectionContext(targetInven).SetLootCountMultiflier(1);
            Managers.SelectionSystem.ExecuteSelection(lootSD, selectionContext);
        });

        Managers.Job.DoFocusJob(job);
    }
}