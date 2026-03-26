using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEncounterContext
{

}

public abstract class BaseEncounterContext<TEncounterData> : BaseEncounterContext where TEncounterData : EncounterDataBase
{
    public TEncounterData EncounterData => encounterData;

    protected TEncounterData encounterData;

    public BaseEncounterContext(TEncounterData encounterData) {
        this.encounterData = encounterData;
    }
}
