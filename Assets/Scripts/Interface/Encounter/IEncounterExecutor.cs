using System;
using UnityEngine;
public interface IEncounterExecutor
{
    Type GetContextType();
    bool CanHandle(object context);
    void ExecuteEncounter(object context);
}

public interface IEncounterExecutor<TEncounterContext, TEncounterSD>
    where TEncounterContext : EncounterContext<TEncounterSD>
    where TEncounterSD : EncounterSD
{
    public void ExecuteEncounter(TEncounterContext encounterSD);
}

public abstract class EncounterContext
{

}

public abstract class EncounterContext<TEncounterSD> : EncounterContext
{
    public TEncounterSD EncounterSD => encounterSD;

    [SerializeField] protected TEncounterSD encounterSD;

    public EncounterContext(TEncounterSD encounterSD) {
        this.encounterSD = encounterSD;
    }
}
