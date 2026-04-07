using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : IInitializable
{
    public Location CurrentLocation
    {
        get => currentLocation;
        set
        {
            var prevLocation = currentLocation;
            currentLocation = value;

            if (currentLocation != prevLocation) {
                OnLocationChanged?.Invoke(currentLocation, prevLocation);
            }
        }
    }
    private LocationUIContainer LocationUIContainer
    {
        get
        {
            if (_container == null) {
                _container = GameObject.FindAnyObjectByType<LocationUIContainer>(FindObjectsInactive.Include);
                if (_container == null) Debug.LogError($"LocationUIContainer is null");
            }

            return _container;
        }
    }

    public bool IsInit => _isInit;

    private Dictionary<string, Location> locationDict = new Dictionary<string, Location>();
    private LocationUIContainer _container;
    private bool _isInit;
    private Location currentLocation;
    private LocationBuilder locationBuilder = new LocationBuilder();

    public event Action<Location, Location> OnLocationChanged;
    public event Action<Location, LocationUI> OnLocationActived;

    public Location RegisterLocation(string locationID, int currentProgress) {
        if (Managers.SD.TryGetSD(locationID, out LocationSD targetSD)) {
            return RegisterLocation(targetSD.ToData(), currentProgress);
        }

        Debug.LogError($"<color=red>location ({locationID}) is null</color>");
        return null;
    }
    public Location RegisterLocation(LocationData locationData, int currentProgress) {
        var location = new Location(locationData);
        if (locationDict.TryAdd(locationData.LocationID, location) == false) {
            Debug.Log($"<color=yellow>{locationData.LocationID}는 이미 존재함</color>");
            return locationDict[locationData.LocationID];
        }

        location.InitProgress(currentProgress, locationData.LocationEventList.Count).Deactivate();
        return location;
    }

    public void UnregisterLocation(LocationSD locationSD) {
        if (locationDict.TryGetValue(locationSD.ID, out var location)) {
            location.Deactivate();
        }
        locationDict.Remove(locationSD.ID);
    }

    public bool TryGetLocation(string locationID, out Location location) {
        location = null;
        if (string.IsNullOrEmpty(locationID)) { Debug.LogError($"<color=red>location null</color>"); return false; }
        if (locationDict.TryGetValue(locationID, out location)) {
            return true;
        }

        Debug.LogError($"<color=red>진행중인 ({locationID}) Location이 없음</color>");
        return false;
    }
    public bool TryGetLocation(LocationSD locationSD, out Location location) {
        if (TryGetLocation(locationSD.ID, out location)) {
            return true;
        }

        Debug.LogError($"<color=red>faile to get location</color>");
        return false;
    }


    public bool TryActivateLocation(string locationID, Action<Location> onActivated = null) {
        if (string.IsNullOrEmpty(locationID)) { Debug.LogError($"<color=red>location id is null</color>"); return false; }

        if (TryGetLocation(locationID, out Location location)) {
            if (TryActivateLocation(location, onActivated)) {
                return true;
            }
            else {
                Debug.LogError($"<color=red>failed to activate location</color>");
            }
        }

        return false;
    }
    public bool TryActivateLocation(LocationSD locationSD, Action<Location> onActivated=null) {
        if (TryGetLocation(locationSD.ID, out Location location)) {
            if (TryActivateLocation(location, onActivated)) {
                return true;
            }
        }

        return false;
    }
    public bool TryActivateLocation(Location location, Action<Location> onActivated =null) {
        if (location == null) { Debug.LogError($"<color=red>location null</color>"); return false; }

        if (LocationUIContainer == null) { Debug.LogError($"<color=red>LocationUIContainer null</color>"); return false; }

        CreateLocationUI(location);
        onActivated?.Invoke(location);
        return true;
    }


    public void DeactivateLocation(string locationID) {
        if (TryGetLocation(locationID, out Location location)) {
            location.Deactivate();
        }
    }
    public void DeactivateLocation(LocationSD locationSD) {
        DeactivateLocation(locationSD.ID);
    }

    public bool TryUnlockLocation(string locationID, int currentProgress=1, Action<Location> onActivated=null) {
        if (string.IsNullOrEmpty(locationID)) return false;
        var location = RegisterLocation(locationID, currentProgress);
        return TryActivateLocation(location, onActivated);
    }
    public void CreateLocation(CoordinateData coordinate) {
        var buildContext = new LocationBuildContext(
            coordinate.LocationUID, 
            coordinate.LocationCoordinate);

        if (locationBuilder.TryBuildLocation(buildContext, out Location newLocation)) {
            CreateLocationUI(newLocation);
        }
    }


    public void Init() {
        if (IsInit) return;

        SetAsDefaultLocation();

        _isInit = true;
    }
    public void Release() {
        Debug.Log($"release need to implement");
    }
    private void SetAsDefaultLocation() {
        string basementID = "basement";
        string houseID = "house";
        TryUnlockLocation(basementID, 0);
        TryUnlockLocation(houseID, 1);
    }
    private void CreateLocationUI(Location location) {
        var locationUI = LocationUIContainer.GetObj();
        locationUI.InitLocation(location);
        location.ClearLocationEvent();
        location.OnLocationStateChanged += locationUI.UpdateUI;
        location.Activate();
        OnLocationActived?.Invoke(location, locationUI);
    }
}
