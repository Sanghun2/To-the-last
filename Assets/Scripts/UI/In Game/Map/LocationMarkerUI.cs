using System;
using System.Net.NetworkInformation;
using BilliotGames;
using UnityEngine;

public class LocationMarkerUI : MarkerUIBase
{
    [SerializeField] Location location;


    public void InitLocation(Location location) {
        if (location == null || location.Data == null) return;

        this.location = location;

        gameObject.name = $"Location UI_{location.Data.LocationUID}";
        InitMarker(location.Data.IconImage, () => OpenPopUp(location));
        SetPosition(location.Data.AnchoredPosition);
    }


    public void UpdateUI(Location.LocationState currentState, Location.LocationState prevState) {
        switch (currentState) {
            case Location.LocationState.Undiscovered:
            case Location.LocationState.Completed:
                CloseUI();
                break;
            case Location.LocationState.Exploring:
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

    private void OpenPopUp(Location location) {
        Managers.UI.OpenUI<LocationInfoPopUpUI>().InitPopUp(location);
    }
}
