using System;
using UnityEngine;

public abstract class EncounterParserBase
{
    public abstract bool TryParse(EncounterSD encounterSD, out EncounterDataBase data);
}


