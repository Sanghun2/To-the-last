using UnityEngine;

public abstract class EncounterBundle<TContext, TSD>
    where TContext : EncounterContext<TSD>
    where TSD : EncounterSD
{
    public abstract IEncounterContextFactory Factory { get; }
    public abstract EncounterExecutorBase<TContext, TSD> Executor { get; }
}
