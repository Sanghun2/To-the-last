using System;
using UnityEngine;

public abstract class EncounterExecutorBase<TEncounterContext, TSD> 
    : IEncounterExecutor, IEncounterExecutor<TEncounterContext, TSD>
    where TEncounterContext : EncounterContext<TSD>
    where TSD : EncounterSD
{
    public abstract void ExecuteEncounter(TEncounterContext encounterSD);

    public Type GetContextType() => typeof(TEncounterContext);
    public bool CanHandle(object context) => context is TEncounterContext;
    public void ExecuteEncounter(object context) {
        //Debug.Log($"execute encounter");
        ExecuteEncounter((TEncounterContext)context);
    }
}
