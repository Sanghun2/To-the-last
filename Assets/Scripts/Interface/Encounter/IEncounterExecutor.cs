using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEncounterExecutor
{
    void ExecuteEncounter(EncounterContextBase context);
}

public interface IEncounterExecutor<TEncounterContext>
    where TEncounterContext : EncounterContextBase
{
    public void ExecuteEncounter(TEncounterContext encounterContext);
}

