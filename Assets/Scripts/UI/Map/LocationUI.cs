using System;
using System.Net.NetworkInformation;
using BilliotGames;
using UnityEngine;

public class LocationUI : UIBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] ContentUI contentUI;
    [SerializeField] LocationSD locationSD;


    public void InitLocation(LocationSD locationSD) {
        if (locationSD == null) return;

        this.locationSD = locationSD;
        SetUIView(locationSD);
        contentUI.SetButtonAction(() => OpenPopUp(locationSD));
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

    #region Pool
    public void Init() {
        InitUI();
    }
    public void Activate() {
        OpenUI();
    }
    public void Release() {
        CloseUI();
    }

    #endregion

    protected override void Start() {
        base.Start();

        InitLocation(locationSD);
    }

    private void OnValidate() {
        if (contentUI != null && locationSD != null) {
            SetUIView(locationSD);
        }
    }

    private void Reset() {
        if (contentUI == null) {
            contentUI = GetComponentInChildren<ContentUI>();
        }
    }

    private void SetUIView(LocationSD locationSD) {
        gameObject.name = $"Location UI_{locationSD.ID}";
        contentUI.SetContentImage(locationSD.IconImage);
    }
    private void OpenPopUp(LocationSD locationSD) {
        Managers.UI.OpenUI<LocationInfoPopUpUI>().InitPopUp(new LocationInfoPopUpData(
                    locationSD,
                    new ActionData[] {
                        new ActionData("확인", () => Managers.UI.CloseUI<LocationInfoPopUpUI>()),
                        new ActionData("진입", null)
                    }));
    }
}
