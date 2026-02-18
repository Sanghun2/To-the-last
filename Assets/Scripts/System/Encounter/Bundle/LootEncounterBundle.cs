using UnityEngine;

public class LootEncounterBundle : EncounterBundle<LootEncounterContext, LootEncounterSD>
{
    public override IEncounterContextFactory Factory => new LootEncounerContextFactory();

    public override EncounterExecutorBase<LootEncounterContext, LootEncounterSD> Executor => new LootEncounterExecutor();
}
