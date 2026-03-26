using System;
using UnityEngine;

public abstract class EncounterExecutor : IEncounterExecutor
{
    public abstract void ExecuteEncounter(EncounterContextBase context);
}

public abstract class EncounterExecutorBase<TEncounterData, TEncounterContext> : EncounterExecutor,
    IEncounterExecutor<TEncounterContext>
    where TEncounterData : EncounterDataBase
    where TEncounterContext : EncounterContextBase<TEncounterData>
{
    public override void ExecuteEncounter(EncounterContextBase context) {
        var converetedContext = context as TEncounterContext;
        if (converetedContext != null) {
            ExecuteEncounter(converetedContext);
        }
        else {
            Debug.LogError($"<color=red>({context.GetType()}) is not ({typeof(TEncounterContext)})</color>");
        }
    }
    public abstract void ExecuteEncounter(TEncounterContext encounterContext);
}
