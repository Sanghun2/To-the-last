using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterContextBase
{

}

public abstract class EncounterContextBase<TEncounterData> : EncounterContextBase 
    where TEncounterData : EncounterDataBase
{
    public TEncounterData EncounterData => encounterData;

    protected TEncounterData encounterData;

    public EncounterContextBase(TEncounterData encounterData) {
        this.encounterData = encounterData;
    }
}
