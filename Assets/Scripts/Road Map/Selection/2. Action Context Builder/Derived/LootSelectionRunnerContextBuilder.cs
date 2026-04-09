using UnityEngine;

public class LootSelectionRunnerContextBuilder : SelectionRunnerContextBuilderBase<LootSelectionRunnerData, LootSelectionRunnerContext>
{
    //public override LootSelectActionContext BuildActionContext(SelectionData data) {  
    //    if (data == null) { Debug.LogError($"<color=red>selection data is null</color>"); return null; }
    //    var targetInven = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID)?.Inventory;

    //    if (targetInven == null) { Debug.LogError($"target inven null"); return null; }

    //    return new LootSelectActionContext(data, targetInven, Managers.Player.PlayerData.CurrentLocationID);
    //}
    

    public override LootSelectionRunnerContext BuildActionContext(LootSelectionRunnerData data) {
        return new LootSelectionRunnerContext(data);
    }
}
