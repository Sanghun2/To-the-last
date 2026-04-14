using System;
using System.Collections.Generic;
using UnityEngine;

public class Exploration
{
    public enum State
    {
        Enterance,
        Exploring,
    }
}

public class ExplorationManager
{
    public LocationBase CurrentLocation
    {
        get
        {
            string currentLocationID = Managers.Player.PlayerData.CurrentLocationID;

            return string.IsNullOrEmpty(currentLocationID) ? null : GetLocation(currentLocationID);
        }
    }
    public LocationUIBase CurrentOpenedUI { get; set; }

    public event Action OnExplorationCompleted;

    public void ContinueToExploreCurrentLocation() {
        var location = CurrentLocation;
        ContinueToExploreLocation(location as ExplorationLocation);
    }
    public void GoToEnterance() {
        var ui = Managers.UI.OpenUI<ExplorationUI>();
        ui.ShowEnterance();
    }

    public void ExitLocation() {
        if (CurrentOpenedUI != null) {
            Managers.UI.CloseUI(CurrentOpenedUI);
            CurrentOpenedUI = null;
        }
        Managers.UI.CloseUI<DialogUI>();
    }

    public void OpenCurrentStorage() {
        if (CurrentLocation == null) { Debug.Log($"location is null"); return; }

        Managers.UI.OpenUI<InventoryViewUI>().ShowInventory(CurrentLocation.LocationUID, Exploration.State.Enterance);
    }


    private void ContinueToExploreLocation(ExplorationLocation location) {
        if (location == null) { Debug.LogError($"<color=red>location null. failed to explore.</color>"); return; }

        if (TryGetNextEncounter(location, out EncounterDataBase nextEncounterEvent)) {
            Debug.Log("encounter 실행 구현");
            Managers.Encounter.ExecuteEncounter(nextEncounterEvent);
            return;
        }

        string[] nextLocationIDs = location.NextLocationIDs;
        for (int i = 0; i < nextLocationIDs.Length; i++) {
            var id = nextLocationIDs[i];
            Managers.Location.TryUnlockMainLocation(id, 1, out var newLocation);
        }
        OnExplorationCompleted?.Invoke();
    }
    private LocationBase GetLocation(string locationUID) {
        if (Managers.Location.TryGetLocation(locationUID, out LocationBase location)) {
            return location;
        }

        return null;
    }
    private bool TryGetNextEncounter(ExplorationLocation location, out EncounterDataBase encounterEvent) {
        IReadOnlyList<EncounterDataBase> eventList = location.LocationEventList;
        Debug.Log($"event count? {eventList.Count}");
        int currentProgress = location.CurrentValue;
        encounterEvent = eventList[currentProgress-1];
        return encounterEvent != null;
    }
}
