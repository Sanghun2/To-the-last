using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;


public class LocationInfoPopUpUI : PopUpUIBase<LocationInfoPopUpData>
{
    [SerializeField] protected TextUI progressText;
    [SerializeField] protected TextUI moveTimeExpectationText;

    public override void InitPopUp(LocationInfoPopUpData popUpData) {
        base.InitPopUp(popUpData);
        LocationData locationData = popUpData.Location.Data;

        int currentProgress = popUpData.Location.CurrentValue;
        int maxProgress = locationData.LocationEventList?.Count ?? -1;
        InitProgressUI(currentProgress, maxProgress);

        ExplorationLocation destination = popUpData.Location;
        ExplorationLocation currentLocation = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID);
        InitMoveTimeUI(currentLocation, destination);
    }
    public void InitPopUp(ExplorationLocation location) {
        var popUpData = new LocationInfoPopUpData(
            location,
            new ActionData[] {
                new ActionData("확인", () => Managers.UI.CloseUI<LocationInfoPopUpUI>()),
                new ActionData(GetButtonText(location), ExecuteLocationEvent(location))
            });
        InitPopUp(popUpData);
    }

    private void InitProgressUI(int currentProgress, int maxProgress) {
        bool showProgress = currentProgress > 0;
        if (showProgress) {
            progressText.SetText($"진행도 {currentProgress}/{maxProgress}");
        }
        progressText.gameObject.SetActive(showProgress);
    }
    private void InitMoveTimeUI(ExplorationLocation currentLocation, ExplorationLocation destination) {
        bool isSamePosition = currentLocation.Equals(destination);
        moveTimeExpectationText.gameObject.SetActive(!isSamePosition);
        if (isSamePosition) return;

        var time = LocationUtility.CalculateDistance(currentLocation.Data.AnchoredPosition, destination.Data.AnchoredPosition).ConvertToTime();
        moveTimeExpectationText.SetText($"{time.hour}시간 {time.minutes}분");
        //Debug.Log($"{currentLocation.TraitID} -> {destination.TraitID}");
    }


    #region UI Info
    private string GetButtonText(ExplorationLocation destination) {
        ExplorationLocation currentSD = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID);
        ExplorationLocation destinationSD = destination;
        if (currentSD.Equals(destinationSD)) {
            return "들어간다";
        }

        return "이동한다";
    }
    private Action ExecuteLocationEvent(ExplorationLocation destination) {
        LocationData currentLocationData = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID)?.Data;
        LocationData endLocationData = destination.Data;
        if (currentLocationData.Equals(endLocationData)) {
            return () => EnterLocation(destination);
        }
        else {
            return () => MoveLocation(currentLocationData, endLocationData);
        }
    }
    private void EnterLocation(ExplorationLocation destination) {
        Managers.UI.CloseUI<LocationInfoPopUpUI>();

        if (destination.LocationUID.Equals(Define.Tag.BASEMENT)) {
            var basementUI = Managers.UI.GetUI<BasementUI>();
            Managers.UI.CloseUI<MapUI>();
            basementUI.OpenUI();
        }
        else {
            var ui = Managers.UI.GetUI<ExplorationUI>();
            ui.InitUI();
            ui.OpenUI();

            ui.InitLocationUI(destination);
            ui.ShowEnterance();
        }
    }
    private void MoveLocation(LocationData currentLocationData, LocationData endLocationData) {
        Managers.UI.CloseUI<LocationInfoPopUpUI>();
        Managers.UI.GetUI<MapUI>().LocationPointer.MovePosition(
            currentLocationData,
            endLocationData,
            callback: () => {
                if (Managers.Location.TryGetLocation(endLocationData.LocationUID, out var destination)) {
                    Managers.Location.CurrentLocation = destination;
                    InitPopUp(destination);
                    Managers.UI.OpenUI(this);
                }
            });
    }
    #endregion
}
