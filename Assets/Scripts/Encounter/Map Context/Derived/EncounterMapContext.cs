using System.Collections.Generic;
using UnityEngine;

public class EncounterMapContext : EncounterMapContextBase
{
    public int MinEncounterCount { get; }
    public int MaxEncounterCount { get; }

    public EncounterMapContext(
        string locationCategoryID, 
        int minEncounterCount,
        int maxEncounterCount,
        IReadOnlyList<EncounterDataBase> encounterDataList=null) 
        : base(locationCategoryID, encounterDataList) {

        MinEncounterCount = minEncounterCount;
        MaxEncounterCount = maxEncounterCount;
    }
}
