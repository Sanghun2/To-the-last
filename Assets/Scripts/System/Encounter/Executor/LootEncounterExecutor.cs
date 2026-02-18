using UnityEngine;

public class LootEncounterContext : EncounterContext<LootEncounterSD>
{
    public LootEncounterContext(LootEncounterSD encounterSD) : base(encounterSD) {
    }
}

public class LootEncounterExecutor : EncounterExecutorBase<LootEncounterContext, LootEncounterSD>
{
    public override void ExecuteEncounter(LootEncounterContext encounterSD) {
        Debug.Log($"loot executed");
    }
}
