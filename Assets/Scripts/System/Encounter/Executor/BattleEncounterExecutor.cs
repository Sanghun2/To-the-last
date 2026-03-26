using UnityEngine;

public class BattleEncounterContext : EncounterContextBase<BattleEncounterData>
{
    public BattleEncounterContext(BattleEncounterData encounterSD) : base(encounterSD) {
    }
}

public class BattleEncounterExecutor : EncounterExecutorBase<BattleEncounterData, BattleEncounterContext>
{
    public override void ExecuteEncounter(BattleEncounterContext encounterSD) {
        Debug.Log($"combat executed");
    }
}
