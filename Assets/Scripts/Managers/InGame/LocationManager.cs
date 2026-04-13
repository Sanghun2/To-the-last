using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LocationManager : IInitializable
{
    public ExplorationLocation CurrentLocation
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
    //private LocationMarkerUIContainer LocationUIContainer
    //{
    //    get
    //    {
    //        if (_container == null) {
    //            _container = GameObject.FindAnyObjectByType<LocationMarkerUIContainer>(FindObjectsInactive.Include);
    //            if (_container == null) Debug.LogError($"LocationUIContainer is null");
    //        }

    //        return _container;
    //    }
    //}

    public bool IsInit => _isInit;

    private Dictionary<string, ExplorationLocation> locationDict = new Dictionary<string, ExplorationLocation>();
    //private LocationMarkerUIContainer _container;
    private bool _isInit;
    private ExplorationLocation currentLocation;
    private LocationBuilder locationBuilder = new LocationBuilder();

    public event Action<ExplorationLocation, ExplorationLocation> OnLocationChanged;
    public event Action<ExplorationLocation, LocationMarkerUI> OnLocationActived;

    public bool TryRegisterLocation(ExplorationLocation newLocation) {
        if (locationDict.TryAdd(newLocation.LocationUID, newLocation) == false) {
            Debug.Log($"<color=yellow>{newLocation.LocationUID}는 이미 존재함</color>");
            return false;
        }

        return true;
    }
    //public ExplorationLocation TryRegisterLocation(string locationSDID, int currentProgress=1) {
    //    if (Managers.RunnerSD.TryGetSD(locationSDID, out LocationSD targetSD)) {
    //        return TryRegisterLocation(targetSD.ToData(), currentProgress);
    //    }

    //    Debug.LogError($"<color=red>newLocation ({locationSDID}) is null</color>");
    //    return null;
    //}
    public bool TryRegisterLocation(LocationData locationData, int currentProgress=1) {
        var location = new ExplorationLocation(locationData);
        if (locationDict.TryAdd(locationData.LocationUID, location) == false) {
            Debug.Log($"<color=yellow>{locationData.LocationUID}는 이미 존재함</color>");
            return false;
        }

        location.InitProgress(currentProgress, locationData.LocationEventList.Count).Deactivate();
        return true;
    }

    public void UnregisterLocation(LocationSD locationSD) {
        if (locationDict.TryGetValue(locationSD.ID, out var location)) {
            location.Deactivate();
        }
        locationDict.Remove(locationSD.ID);
    }

    public bool TryGetLocation(string locationUID, out ExplorationLocation location) {
        location = null;
        if (string.IsNullOrEmpty(locationUID)) { Debug.LogError($"<color=red>location null</cTryGetLocationolor>"); return false; }
        if (locationDict.TryGetValue(locationUID, out location)) {
            return true;
        }

        Debug.LogError($"<color=red>진행중인 ({locationUID}) Location이 없음</color>");
        return false;
    }
    public bool TryGetLocation(LocationSD locationSD, out ExplorationLocation location) {
        if (TryGetLocation(locationSD.ID, out location)) {
            return true;
        }

        Debug.LogError($"<color=red>faile to get location</color>");
        return false;
    }


    public bool TryActivateLocation(string locationID, Action<ExplorationLocation> onActivated = null) {
        if (string.IsNullOrEmpty(locationID)) { Debug.LogError($"<color=red>location id is null</color>"); return false; }

        if (TryGetLocation(locationID, out ExplorationLocation location)) {
            if (TryActivateLocation(location, onActivated)) {
                return true;
            }
            else {
                Debug.LogError($"<color=red>failed to activate location</color>");
            }
        }

        return false;
    }
    public bool TryActivateLocation(LocationSD locationSD, Action<ExplorationLocation> onActivated=null) {
        if (TryGetLocation(locationSD.ID, out ExplorationLocation location)) {
            if (TryActivateLocation(location, onActivated)) {
                return true;
            }
        }

        return false;
    }
    public bool TryActivateLocation(ExplorationLocation location, Action<ExplorationLocation> onActivated =null) {
        if (location == null) { Debug.LogError($"<color=red>location null</color>"); return false; }

        //if (LocationUIContainer == null) { Debug.LogError($"<color=red>LocationUIContainer null</color>"); return false; }

        CreateLocationUI(location);
        onActivated?.Invoke(location);
        return true;
    }


    public void DeactivateLocation(string locationID) {
        if (TryGetLocation(locationID, out ExplorationLocation location)) {
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

        return CreateNewLocationCoordinate(randomLocationSD);
    }
    public CoordinateData CreateNewLocationCoordinate(LocationInfoSD locationInfo) {
        if (locationInfo.CategoryID.Equals(Define.Tag.BASEMENT)) { Debug.LogError($"<color=red>basement can not be new location</color>"); return null; }

        string locationUID = CreateNewLocationUID(locationInfo);
        string locationName = CraeteLocationName(locationInfo);

        return new CoordinateData(
            locationUID,
            locationInfo.ID,
            locationName,
            CreateRandomCoordinate(),
            CreateNewHz(),
            locationInfo.IconImage
            );
    }


    private float CreateNewHz() {
        var targetHz = Random.Range(Define.Value.MIN_HZ_VALUE, Define.Value.MAX_HZ_VALUE);
        return Mathf.Round(targetHz * 10f) / 10f;
    }
    private Vector2 CreateRandomCoordinate() {
        return LocationUtility.GenerateRandomLocationCoordinate();
    }
    private string CreateNewLocationUID(LocationInfoSD randomLocationSD) {
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



    public bool TryUnlockMainLocation(string locationSDID, int currentProgress, out ExplorationLocation newLocation, Action<ExplorationLocation> onActivated = null) {
        newLocation = null;
        if (string.IsNullOrEmpty(locationSDID)) return false;

        if (!TryGetLocationSD(locationSDID, out LocationSD locationSD)) { return false; }

        var buildContext = CreateLocationBuildContext(locationSD);
        buildContext.SetProgress(currentProgress);
        if (locationBuilder.TryBuildLocation(buildContext, out newLocation)) {
            if (!TryRegisterLocation(newLocation)) return false;
            return TryActivateLocation(newLocation, onActivated);
        }

        return false;   
    }
    public bool TryUnlockSubLocation(CoordinateData coordinate, out ExplorationLocation newLocation) {
        newLocation = null;

        // 최종 보스 + 최종 보상
        var finalEncounterList = new List<EncounterDataBase>();

        var buildContext = new LocationBuildContext(
            coordinate.LocationUID, 
            coordinate.LocationCategoryID,
            coordinate.LocationName,
            coordinate.AnchoredPosition,
            finalEncounterList);

        if (!locationBuilder.TryBuildLocation(buildContext, out newLocation)) { Debug.LogError($"<color=red>failed to build new location</color>"); return false; }

        if (!TryRegisterLocation(newLocation)) return false;
        TryActivateLocation(newLocation.LocationUID);
        return true;
    }

    private LocationBuildContext CreateLocationBuildContext(LocationSD locationSD) {
        IReadOnlyList<EncounterDataBase> essentialEncounterList = Managers.Encounter.ConvertToEncounterData(locationSD.EssentialLocationEventList);
        return new LocationBuildContext(
            locationSD.ID,
            locationSD.CategoryID,
            locationSD.DisplayText,
            LocationUtility.GenerateRandomLocationCoordinate(),
            essentialEncounterList);
    }

    private bool TryGetLocationSD(string locationSDID, out LocationSD locationSD) {
        if (Managers.SD.TryGetSD(locationSDID, out locationSD)) {
            return true;
        }

        Debug.LogError($"<color=red>({locationSDID}) is not exist</color>");
        return false;
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
        string basementID = Define.Tag.BASEMENT;
        string houseID = "house";
        TryUnlockMainLocation(basementID, 0, out var basement);
        TryUnlockMainLocation(houseID, 1, out var house);
    }
    private void CreateLocationUI(ExplorationLocation location) {
        if (Managers.MapMarker.TryGet<LocationMarkerUI, LocationMarkerUIContainer>(out var container)) {
            LocationMarkerUI locationUI = container.GetObj();
            locationUI.InitLocation(location);
            location.ClearLocationEvent();
            location.OnLocationStateChanged += locationUI.UpdateUI;
            location.Activate();
            OnLocationActived?.Invoke(location, locationUI);
        }
    }
}
