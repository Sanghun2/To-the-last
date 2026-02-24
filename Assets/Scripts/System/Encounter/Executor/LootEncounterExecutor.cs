using UnityEngine;

public class LootEncounterContext : EncounterContext<LootEncounterSD>
{
    public LootEncounterContext(LootEncounterSD encounterSD) : base(encounterSD) {
    }
}

public class LootEncounterExecutor : EncounterExecutorBase<LootEncounterContext, LootEncounterSD>
{
    public override void ExecuteEncounter(LootEncounterContext encounterContext) {
        var explorationUI = Managers.UI.GetUI<ExplorationUI>();
        if (explorationUI.IsOpened == false) Managers.UI.OpenUI(explorationUI);

        explorationUI.ShowSituation(encounterContext.EncounterSD);
    }
}
