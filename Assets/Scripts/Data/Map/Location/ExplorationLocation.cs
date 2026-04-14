using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[Serializable]
public class ExplorationLocation : LocationBase, IValue<int>
{
    public int CurrentValue => currentProgress;
    public int MaxValue => maxProgress;

    public string LocationCategoryID => locationCategoryID;
    public string[] NextLocationIDs => nextLocationIDs;
    public IReadOnlyList<EncounterDataBase> LocationEventList { get; }


    [SerializeField][HideInInspector] string locationUID;
    [SerializeField] string locationCategoryID;

    [SerializeField] int currentProgress;
    [SerializeField] int maxProgress;
    private string[] nextLocationIDs;

    public ExplorationLocation(
        LocationData data, 
        IReadOnlyList<EncounterDataBase> locationEventList,
        string[] nextLocationIDs=null) : base(data) {

        LocationEventList = locationEventList;
    }
    //public ExplorationLocation(LocationData locationData) {
    //    this.data = locationData;
    //    locationUID = locationData.LocationUID;
    //    locationCategoryID = locationData.LocationCategoryID;
    //    _currentState = LocationState.Inactive;
    //    nextLocationIDs = locationData.NextLocationIDs;
    //    displayName = locationData.DisplayName;
    //    AnchoredPosition = locationData.AnchoredPosition;
    //}

    //public ExplorationLocation(CoordinateData coordinate) {
    //    data = new LocationData(coordinate);
    //    locationUID = coordinate.LocationUID;
    //    displayName = coordinate.LocationName;
    //    _currentState = LocationState.Inactive;
    //    AnchoredPosition = coordinate.AnchoredPosition;
    //}

    public event Action<int, int> OnProgressChanged;

    public ExplorationLocation InitProgress(int current, int max) {
        currentProgress = current;
        maxProgress = max;
        OnProgressChanged?.Invoke(current, max);
        return this;
    }
    public void ChangeProgress(int deltaValue) {
        currentProgress += deltaValue;
        OnProgressChanged?.Invoke(currentProgress, maxProgress);

        if (currentProgress == maxProgress) {
            CurrentState = LocationState.Completed;
        }
    }

    public override void ClearLocationEvent() {
        base.ClearLocationEvent();
        OnProgressChanged = null;
    }

    protected override string GetInventoryName() {
        return "보관함";
    }
}
