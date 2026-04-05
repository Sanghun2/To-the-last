using UnityEngine;

public class LootSelectActionContextBuilder : SelectActionContextBuilderBase<LootSelectionData, LootSelectActionContext>
{
    public override bool TryBuildActionContext(LootSelectionData data, out LootSelectActionContext context) {
        context = null;

        if (data == null) { Debug.LogError($"<color=red>selection data is null</color>"); return false; }
        var targetInven = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID)?.Inventory;

        if (targetInven == null) { Debug.LogError($"target inven null"); return false; }

        context = new LootSelectActionContext(data, targetInven, Managers.Player.PlayerData.CurrentLocationID);
        return true;
    }
}
