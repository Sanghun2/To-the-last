using System;
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
    public Location CurrentLocation
    {
        get
        {
            string currentLocationID = Managers.Player.PlayerData.CurrentLocationID;

            return string.IsNullOrEmpty(currentLocationID) ? null : GetLocation(currentLocationID);
        }
    }

    public event Action OnExplorationCompleted;

    public void ContinueToExploreCurrentLocation() {
        var location = CurrentLocation;
        ContinueToExploreLocation(location);
    }
    public void GoToEnterance() {
        var ui = Managers.UI.OpenUI<ExplorationUI>();
        ui.ShowEnterance();
    }

    public void ExitLocation() {
        Managers.UI.CloseUI<ExplorationUI>();
        Managers.UI.CloseUI<DialogUI>();
    }

    public void OpenCurrentStorage() {
        if (CurrentLocation == null) { Debug.Log($"location is null"); return; }

        Managers.UI.OpenUI<LocationInventoryUI>().ShowInventory(CurrentLocation.LocationUID, Exploration.State.Enterance);
    }


    private void ContinueToExploreLocation(Location location) {
        if (location == null) { Debug.LogError($"<color=red>location null. failed to explore.</color>"); return; }

        if (TryGetNextEncounter(location, out EncounterInfo nextEncounterEvent)) {
            Debug.Log("encounter 실행 구현");
            Managers.Encounter.ExecuteEncounter(nextEncounterEvent.EncounterSD);
            return;
        }

        string nextLocation = location.NextLocationID;
        Managers.Location.TryUnlockLocationBySD(nextLocation);
        OnExplorationCompleted?.Invoke();
    }
    private Location GetLocation(string locationID) {
        if (Managers.Location.TryGetLocation(locationID, out Location location)) {
            return location;
        }

        return null;
    }
    private bool TryGetNextEncounter(Location location, out EncounterInfo encounterEvent) {
        var eventList = location.Data.LocationEventList;
        int currentProgress = location.CurrentValue;
        encounterEvent = eventList[currentProgress-1];
        return encounterEvent != null;
    }
}
