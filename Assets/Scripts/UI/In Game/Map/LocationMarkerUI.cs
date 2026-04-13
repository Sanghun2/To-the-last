using System;
using System.Net.NetworkInformation;
using BilliotGames;
using UnityEngine;

public class LocationMarkerUI : MarkerUIBase
{
    [SerializeField] ExplorationLocation location;


    public void InitLocation(ExplorationLocation location) {
        if (location == null || location.Data == null) return;

        this.location = location;

        gameObject.name = $"Location UI_{location.Data.LocationUID}";
        InitMarker(location.Data.IconImage, () => OpenPopUp(location));
        SetPosition(location.Data.AnchoredPosition);
    }


    public void UpdateUI(ExplorationLocation.LocationState currentState, ExplorationLocation.LocationState prevState) {
        switch (currentState) {
            case ExplorationLocation.LocationState.Inactive:
            case ExplorationLocation.LocationState.Completed:
                CloseUI();
                break;
            case ExplorationLocation.LocationState.Active:
                OpenUI();
                break;
            default:
                break;
        }
    }
    public void SaveCurrentLocationPosition() {
        if (location == null) { Debug.Log($"target is empty. save location skipped."); return; }
        var rt = GetComponent<RectTransform>();
        var targetPos = rt.anchoredPosition;

        if (Managers.SD.TryGetSD<LocationSD>(location.Data.LocationUID, out var locationSD)) {
            locationSD.SetAnchoredPosition(targetPos);
        }
    }



    protected override void Start() {
        base.Start();

        InitLocation(location);
    }

    private void OpenPopUp(ExplorationLocation location) {
        Managers.UI.OpenUI<LocationInfoPopUpUI>().InitPopUp(location);
    }
}
