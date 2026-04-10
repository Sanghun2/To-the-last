using System;
using UnityEngine;

public abstract class EncounterDataParserBase
{
    public abstract EncounterDataBase ParseData(EncounterSDBase encounterSD);
}

public abstract class EncounterDataParserBase<TSD, TData> : EncounterDataParserBase
    where TSD : EncounterSDBase
    where TData : EncounterDataBase
{
    public abstract TData ParseData(TSD encounterSD);

    public override EncounterDataBase ParseData(EncounterSDBase encounterSD) {
        if (encounterSD is TSD tsd) {
            return ParseData(tsd);
        }

        Debug.LogError($"<color=red>encounterSD is not type of ({typeof(TSD)})</color>");
        return null;
    }
}


