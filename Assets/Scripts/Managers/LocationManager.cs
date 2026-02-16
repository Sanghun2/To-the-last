using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager
{
    private LocationUIContainer LocationUIContainer
    {
        get
        {
            if (_container == null) {
                _container = GameObject.FindAnyObjectByType<LocationUIContainer>();
            }

            return _container;
        }
    }

    private Dictionary<string, Location> locationDict = new Dictionary<string, Location>();
    private LocationUIContainer _container;

    public void AddLocation(LocationSD locationSD) {
        var location = new Location(locationSD);
        if (locationDict.TryAdd(locationSD.ID, location) == false) {
            Debug.LogError($"<color=red>{locationSD.ID}는 이미 존재함</color>");
        }
    }
    public void RemoveLocation(LocationSD locationSD) {
        locationDict.Remove(locationSD.ID);
    }

    public bool TryGetLocation(LocationSD locationSD, out Location location) {
        location = null;
        if (locationSD == null) { Debug.LogError($"<color=red>location null</color>"); return false; }
        if (locationDict.TryGetValue(locationSD.ID, out location)) {
            return true;
        }

        Debug.LogError($"<color=red>진행중인 {locationSD.ID} Location이 없음</color>");
        return false;
    }

    public bool TryActivateLocation(LocationSD locationSD, Action<Location> onActivated =null) {
        if (locationSD == null) { Debug.LogError($"<color=red>location null</color>"); return false; }
        if (TryGetLocation(locationSD, out Location location)) {
            var locationUI = LocationUIContainer.GetObj();
            locationUI.InitLocation(locationSD);
            location.ClearEvent();
            location.OnStateChanged += locationUI.UpdateUI;
            location.Activate();
            onActivated?.Invoke(location);
            return true;
        }
        else {
            Debug.LogError($"<color=red>{locationSD.ID} Location은 현재 activate 불가능한 조건</color>");
            return false;
        }
    }
    public void DeactivateLocation(LocationSD locationSD) {
        if (TryGetLocation(locationSD, out Location location)) {
            location.Deactivate();
        }
    }
}
