using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public Location RegisterLocation(Location newLocation) {
        if (locationDict.TryAdd(newLocation.LocationUID, newLocation) == false) {
            Debug.Log($"<color=yellow>{newLocation.LocationUID}는 이미 존재함</color>");
            return locationDict[newLocation.LocationUID]; ;
        }

        return newLocation;
    }
    //public Location RegisterLocation(string locationSDID, int currentProgress=1) {
    //    if (Managers.SD.TryGetSD(locationSDID, out LocationSD targetSD)) {
    //        return RegisterLocation(targetSD.ToData(), currentProgress);
    //    }

    //    Debug.LogError($"<color=red>newLocation ({locationSDID}) is null</color>");
    //    return null;
    //}
    public Location RegisterLocation(LocationData locationData, int currentProgress=1) {
        var location = new Location(locationData);
        if (locationDict.TryAdd(locationData.LocationUID, location) == false) {
            Debug.Log($"<color=yellow>{locationData.LocationUID}는 이미 존재함</color>");
            return locationDict[locationData.LocationUID];
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

    public CoordinateData CreateNewLocationCoordinate() {
        if (!Managers.SD.TryGetContainer<LocationInfoSD>(out var container)) { Debug.LogError($"<color=red>couldn't find location info container</color>"); return null; }
        var locationList = container.SDDict.Values.Where(
            l =>
            !l.ID.Equals(Define.Tag.BASEMENT) &&
            !l.ID.Equals(Define.Tag.AMUSEMENTPARK) && 
            !l.ID.Equals(Define.Tag.BROADCAST_STATION)
            ).ToList();

        LocationInfoSD randomLocationSD = locationList[UnityEngine.Random.Range(0, locationList.Count)];

        string locationUID = CreateLocationUID(randomLocationSD);
        string locationName = CraeteLocationName(randomLocationSD);

        return new CoordinateData(
            locationUID,
            randomLocationSD.ID,
            locationName,
            CreateRandomCoordinate(),
            CreateNewHz()
            );
    }


    private float CreateNewHz() {
        var targetHz = Random.Range(Define.Value.MIN_HZ_VALUE, Define.Value.MAX_HZ_VALUE);
        return Mathf.Round(targetHz * 10f) / 10f;
    }
    private Vector2 CreateRandomCoordinate() {
        return new Vector2(Random.Range(-350, 350), Random.Range(-550, 420));
    }
    private string CreateLocationUID(LocationInfoSD randomLocationSD) {
        return $"{randomLocationSD.ID}-{Guid.NewGuid()}";
    }
    private string CraeteLocationName(LocationInfoSD randomLocationSD) {
        var regions = new string[] {
            "염창",
            "신도림",
            "목동",
            "양평",
            "화곡",
            "영등포",
        };
        var randomRegion = regions[UnityEngine.Random.Range(0, regions.Length)];
        return $"{randomRegion} {randomLocationSD.DisplayText}";
    }



    public bool TryUnlockLocationBySD(string locationSDID, int currentProgress=1, Action<Location> onActivated=null) {
        if (string.IsNullOrEmpty(locationSDID)) return false;

        if (!TryGetLocationSD(locationSDID, out LocationSD locationSD)) { return false; }

        var location = new Location(locationSD.ToData());
        RegisterLocation(location);
        return TryActivateLocation(location, onActivated);
    }

    private bool TryGetLocationSD(string locationSDID, out LocationSD locationSD) {
        if (Managers.SD.TryGetSD(locationSDID, out locationSD)) {
            return true;
        }

        Debug.LogError($"<color=red>({locationSDID}) is not exist</color>");
        return false;
    }

    public void UnlockLocation(CoordinateData coordinate) {
        var buildContext = new LocationBuildContext(
            coordinate.LocationUID, 
            coordinate.LocationCategoryID,
            coordinate.AnchoredPosition);

        if (locationBuilder.TryBuildLocation(buildContext, out Location newLocation)) {
            CreateLocationUI(newLocation);
            RegisterLocation(newLocation);
            TryActivateLocation(newLocation.LocationUID);
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
        TryUnlockLocationBySD(basementID, 0);
        TryUnlockLocationBySD(houseID, 1);
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
