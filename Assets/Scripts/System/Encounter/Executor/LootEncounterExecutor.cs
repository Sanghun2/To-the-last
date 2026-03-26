using UnityEngine;

public class LootEncounterContext : EncounterContextBase<LootEncounterData>
{
    public LootEncounterContext(LootEncounterData encounterData) : base(encounterData) {
    }
}

public class LootEncounterExecutor : EncounterExecutorBase<LootEncounterData, LootEncounterContext>
{
    public override void ExecuteEncounter(LootEncounterContext encounterContext) {
        var explorationUI = Managers.UI.GetUI<ExplorationUI>();
        if (explorationUI.IsOpened == false) Managers.UI.OpenUI(explorationUI);

        explorationUI.ShowSituation(encounterContext.EncounterData);
    }
}
