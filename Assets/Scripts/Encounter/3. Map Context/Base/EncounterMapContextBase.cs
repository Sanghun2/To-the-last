using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterMapContextBase
{
    public string LocationCategoryID { get; }
    public IReadOnlyList<EncounterDataBase> EssentialEncounterList { get; }

    public EncounterMapContextBase(
        string locationCategoryID, 
        IReadOnlyList<EncounterDataBase> encounterDataList=null) {
        LocationCategoryID = locationCategoryID;
        EssentialEncounterList = encounterDataList;
    }
}
