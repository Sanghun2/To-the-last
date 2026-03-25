using System;
using System.Net.NetworkInformation;
using BilliotGames;
using UnityEngine;

public class LocationUI : UIBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] ContentUI contentUI;
    [SerializeField] Location location;


    public void InitLocation(Location location) {
        if (location == null || location.Data == null) return;

        this.location = location;
        SetUIView(location.Data);
        SetPosition(location.Data.AnchoredPosition);
        contentUI.SetButtonAction(() => OpenPopUp(location));
    }


    public void UpdateUI(Location.State currentState, Location.State prevState) {
        switch (currentState) {
            case Location.State.Undiscovered:
            case Location.State.Completed:
                CloseUI();
                break;
            case Location.State.Exploring:
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

        if (Managers.SD.TryGetSD<LocationSD>(location.Data.LocationID, out var locationSD)) {
            locationSD.SetAnchoredPosition(targetPos);
        }
    }


    #region Pool
    public void Init() {
        InitUI();
    }
    public void Activate() {
        OpenUI();
    }
    public void Return() {
        CloseUI();
    }

    #endregion

    protected override void Start() {
        base.Start();

        InitLocation(location);
    }

    private void Reset() {
        if (contentUI == null) {
            contentUI = GetComponentInChildren<ContentUI>();
        }
    }

    private void SetUIView(LocationData locationData) {
        if (locationData == null) { Debug.Log($"location sd null. location 정보 set 불가"); return; }
        gameObject.name = $"Location UI_{locationData.LocationID}";
        contentUI.SetContentImage(locationData.IconImage);
    }
    private void SetPosition(Vector2 anchoredPosition) {
        GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }
    private void OpenPopUp(Location location) {
        Managers.UI.OpenUI<LocationInfoPopUpUI>().InitPopUp(location);
    }
}
