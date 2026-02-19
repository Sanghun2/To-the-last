using UnityEngine;

public class BattleEncounterContext : EncounterContext<BattleEncounterSD>
{
    public BattleEncounterContext(BattleEncounterSD encounterSD) : base(encounterSD) {
    }
}

public class BattleEncounterExecutor : EncounterExecutorBase<BattleEncounterContext, BattleEncounterSD>
{
    public override void ExecuteEncounter(BattleEncounterContext encounterSD) {
        Debug.Log($"combat executed");
    }
}
