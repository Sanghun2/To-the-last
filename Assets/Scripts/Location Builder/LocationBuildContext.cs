using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationBuildContext
{
    public string LocationUID { get; }
    public string LocationCategoryID { get; }
    public Vector2 AnchoredPosition { get; }
    public string DisplayName { get; }
    public IReadOnlyList<EncounterDataBase> EncounterDataList { get; }
    public int CurrentProgress => _currentProgress;

    private int _currentProgress;

    public LocationBuildContext(
        string locationUID, 
        string locationCategoryID, 
        string displayName,
        Vector2 locationCoordinate,
        IReadOnlyList<EncounterDataBase> encounterDataList=null) {

        LocationUID = locationUID;
        LocationCategoryID = locationCategoryID;
        DisplayName = displayName;
        AnchoredPosition = locationCoordinate;
        EncounterDataList = encounterDataList;
    }

    public void SetProgress(int currentProcess) {
        _currentProgress = currentProcess;
    }
}
