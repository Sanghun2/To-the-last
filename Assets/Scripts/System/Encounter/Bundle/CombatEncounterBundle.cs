using UnityEngine;

public class CombatEncounterBundle : EncounterBundle<CombatEncounterContext, CombatEncounterSD>
{
    public override IEncounterContextFactory Factory => new CombatEncounterContextFactory();

    public override EncounterExecutorBase<CombatEncounterContext, CombatEncounterSD> Executor =>
        new CombatEncounterExecutor();
}
