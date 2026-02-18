using UnityEngine;

public class SpecialEncounterContext : EncounterContext<SpecialEncounterSD>
{
    public SpecialEncounterContext(SpecialEncounterSD encounterSD) : base(encounterSD) {
    }
}

public class SpecialEncounterExecutor : EncounterExecutorBase<SpecialEncounterContext,  SpecialEncounterSD>
{
    public override void ExecuteEncounter(SpecialEncounterContext encounterSD) {
        throw new System.NotImplementedException();
    }
}
