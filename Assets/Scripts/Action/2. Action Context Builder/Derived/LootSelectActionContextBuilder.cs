using UnityEngine;

public class LootSelectActionContextBuilder : SelectActionContextBuilderBase<LootSelectionRunnerData, LootSelectActionContext>
{
    public override LootSelectActionContext BuildActionContext(LootSelectionRunnerData data) {  
        if (data == null) { Debug.LogError($"<color=red>selection data is null</color>"); return null; }
        var targetInven = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID)?.Inventory;

        if (targetInven == null) { Debug.LogError($"target inven null"); return null; }

        return new LootSelectActionContext(data, targetInven, Managers.Player.PlayerData.CurrentLocationID);
    }
}
