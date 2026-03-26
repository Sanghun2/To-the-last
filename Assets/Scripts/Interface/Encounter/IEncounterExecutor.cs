using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEncounterExecutor
{
    void ExecuteEncounter(BaseEncounterContext context);
}

public interface IEncounterExecutor<TEncounterContext>
    where TEncounterContext : BaseEncounterContext
{
    public void ExecuteEncounter(TEncounterContext encounterContext);
}

