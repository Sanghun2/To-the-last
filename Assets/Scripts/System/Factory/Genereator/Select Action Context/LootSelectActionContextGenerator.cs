using UnityEngine;

public class LootSelectActionContextGenerator : SelectActionContextGenerator
{
    public override bool TryGenerateContext(SelectionSD selectionSD, out SelectActionContext context) {
        context = null;
        if (selectionSD == null) { Debug.LogError($"selection SD null"); return false; }

        var targetInven = Managers.Player.PlayerData.CurrentLocationID.ToLocation()?.Inventory;
        if (targetInven == null) { Debug.LogError($"target inven null"); return false; }

        context = new LootSelectActionContext(selectionSD, targetInven);
        return true;
    }
}
