using System;
using BilliotGames;
using UnityEngine;

[Serializable]
public class ExplorationLocation : LocationBase, IValue<int>
{
    public int CurrentValue => currentProgress;
    public int MaxValue => maxProgress;

    public string LocationCategoryID => locationCategoryID;
    public string NextLocationID => nextLocationID;


    [SerializeField][HideInInspector] string locationUID;
    [SerializeField] string locationCategoryID;

    [SerializeField] int currentProgress;
    [SerializeField] int maxProgress;
    private string nextLocationID;

    public ExplorationLocation(LocationData data) : base(data) {

    }
    //public ExplorationLocation(LocationData locationData) {
    //    this.data = locationData;
    //    locationUID = locationData.LocationUID;
    //    locationCategoryID = locationData.LocationCategoryID;
    //    _currentState = LocationState.Inactive;
    //    nextLocationID = locationData.NextLocationID;
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
}
