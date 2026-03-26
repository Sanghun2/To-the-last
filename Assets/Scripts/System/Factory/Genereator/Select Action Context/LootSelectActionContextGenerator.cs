using UnityEngine;

public class LootSelectActionContextGenerator : SelectActionContextBuilderBase
{
    public override bool TryBuildContext(SelectionDataBase selectionData, out SelectActionContext context) {
        context = null;
        if (selectionData == null) { Debug.LogError($"<color=red>selection data is null</color>"); return false; }

        var targetInven = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID)?.Inventory;
        if (targetInven == null) { Debug.LogError($"target inven null"); return false; }

        //context = new LootSelectActionContext(selectionData, targetInven);
        return true;
    }
}
