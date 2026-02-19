using UnityEngine;

public class BattleEncounterBundle : EncounterBundle<BattleEncounterContext, BattleEncounterSD>
{
    public override IEncounterContextFactory Factory => new BattleEncounterContextFactory();

    public override EncounterExecutorBase<BattleEncounterContext, BattleEncounterSD> Executor =>
        new BattleEncounterExecutor();
}
