using UnityEngine;

public class SpecialEncounterBundle : EncounterBundle<SpecialEncounterContext, SpecialEncounterSD>
{
    public override IEncounterContextFactory Factory => new SpecialEncounterContextFactory();

    public override EncounterExecutorBase<SpecialEncounterContext, SpecialEncounterSD> Executor => new SpecialEncounterExecutor();
}
