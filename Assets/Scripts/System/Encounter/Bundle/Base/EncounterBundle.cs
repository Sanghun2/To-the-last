using UnityEngine;

public abstract class EncounterBundle<TContext, TSD> : MonoBehaviour
    where TContext : EncounterContext<TSD>
    where TSD : EncounterSD
{
    public abstract IEncounterContextFactory Factory { get; }
    public abstract EncounterExecutorBase<TContext, TSD> Executor { get; }
}
