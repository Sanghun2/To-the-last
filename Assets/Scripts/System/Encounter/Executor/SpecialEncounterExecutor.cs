using UnityEngine;

public class SpecialEncounterContext : EncounterContextBase<SpecialEncounterData>
{
    public SpecialEncounterContext(SpecialEncounterData encounterData) : base(encounterData) {
    }
}

public class SpecialEncounterExecutor : EncounterExecutorBase<SpecialEncounterData, SpecialEncounterContext>
{
    public override void ExecuteEncounter(SpecialEncounterContext encounterData) {
        throw new System.NotImplementedException();
    }
}
