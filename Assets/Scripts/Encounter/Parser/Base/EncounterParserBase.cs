using System;
using UnityEngine;

public abstract class EncounterParserBase
{
    public abstract bool TryParse(EncounterSDBase encounterSD, out EncounterDataBase data);
}


