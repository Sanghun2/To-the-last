using UnityEngine;

public class CombatEncounterContext : EncounterContext<CombatEncounterSD>
{
    public CombatEncounterContext(CombatEncounterSD encounterSD) : base(encounterSD) {
    }
}

public class CombatEncounterExecutor : EncounterExecutorBase<CombatEncounterContext, CombatEncounterSD>
{
    public override void ExecuteEncounter(CombatEncounterContext encounterSD) {
        Debug.Log($"combat executed");
    }
}
