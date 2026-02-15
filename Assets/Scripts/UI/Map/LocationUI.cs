using System.Net.NetworkInformation;
using BilliotGames;
using UnityEngine;

public class LocationUI : UIBase
{
    [SerializeField] ContentUI contentUI;
    [SerializeField] LocationSD locationSD;

    public void InitLocation(LocationSD locationSD) {
        if (locationSD == null) return;

        this.locationSD = locationSD;
        contentUI.SetContentImage(locationSD.IconImage);
        contentUI.SetButtonAction(() => OpenPopUp(locationSD));
    }


    protected override void Start() {
        base.Start();

        InitLocation(locationSD);
    }

    private void OnValidate() {
        if (contentUI != null) {
            contentUI.SetContentImage(locationSD.IconImage);
            gameObject.name = $"Location UI_{locationSD.ID}";
        }
    }

    private void Reset() {
        if (contentUI == null) {
            contentUI = GetComponentInChildren<ContentUI>();
        }
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
