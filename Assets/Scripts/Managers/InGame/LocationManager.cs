using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : IInitializable
{
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

    public Location RegisterLocation(string locationID, int currentProgress) {
        if (Managers.SD.TryGetSD(locationID, out LocationSD targetSD)) {
            return RegisterLocation(targetSD, currentProgress);
        }

        Debug.LogError($"<color=red>location ({locationID}) is null</color>");
        return null;
    }
    public Location RegisterLocation(LocationSD locationSD, int currentProgress) {
        var location = new Location(locationSD);
        if (locationDict.TryAdd(locationSD.ID, location) == false) {
            Debug.Log($"<color=yellow>{locationSD.ID}는 이미 존재함</color>");
            return locationDict[locationSD.ID];
        }

        location.InitProgress(currentProgress, locationSD.LocationEventList.Count).Deactivate();
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

    public bool UnlockLocation(string locationID, int currentProgress=1, Action<Location> onActivated=null) {
        var location = RegisterLocation(locationID, currentProgress);
        return TryActivateLocation(location, onActivated);
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

        var locationUI = LocationUIContainer.GetObj();
        locationUI.InitLocation(location);
        location.ClearLocationEvent();
        location.OnStateChanged += locationUI.UpdateUI;
        location.Activate();
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
        UnlockLocation(basementID, 0);
        UnlockLocation(houseID, 1);
    }
}
