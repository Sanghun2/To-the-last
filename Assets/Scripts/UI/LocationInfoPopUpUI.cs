using System;
using BilliotGames;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;


public class LocationInfoPopUpData : PopUpData
{
    public Location Location => location;

    [SerializeField] Location location;

    public LocationInfoPopUpData(string title, string description, ActionData[] buttonActions) : base(title, description, buttonActions) {
    }


    public LocationInfoPopUpData(Location location, ActionData[] buttonActions) 
        : base (location.LocationSD.DisplayName, location.LocationSD.StoryDescription, buttonActions){
        this.location = location;
    }
}

public class LocationInfoPopUpUI : PopUpUIBase<LocationInfoPopUpData>
{
    [SerializeField] protected Image locationImage;
    [SerializeField] protected TextUI progressText;
    [SerializeField] protected TextUI moveTimeExpectationText;

    public override void InitPopUp(LocationInfoPopUpData popUpData) {
        base.InitPopUp(popUpData);
        var sd = popUpData.Location.LocationSD;
        locationImage.sprite = sd.MainImage;

        int currentProgress = popUpData.Location.CurrentValue;
        int maxProgress = sd.LocationEventList.Count;
        InitProgressUI(currentProgress, maxProgress);

        LocationSD destination = popUpData.Location.LocationSD;
        LocationSD currentLocation = Managers.Player.PlayerData.CurrentLocationID.ToLocationSD();
        InitMoveTimeUI(currentLocation, destination);
    }
    public void InitPopUp(Location location) {
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
    private void InitMoveTimeUI(LocationSD currentLocation, LocationSD destination) {
        bool isSamePosition = currentLocation.Equals(destination);
        moveTimeExpectationText.gameObject.SetActive(!isSamePosition);
        if (isSamePosition) return;

        var time = LocationUtility.CalculateDistance(currentLocation, destination).ConvertToTime();
        moveTimeExpectationText.SetText($"{time.hour}시간 {time.minutes}분");
        Debug.Log($"{currentLocation.ID} -> {destination.ID}");
    }


    #region UI Info
    private string GetButtonText(Location destination) {
        LocationSD currentSD = Managers.Player.PlayerData.CurrentLocationID.ToLocationSD();
        LocationSD destinationSD = destination.LocationSD;
        if (currentSD.Equals(destinationSD)) {
            return "들어간다";
        }

        return "이동한다";
    }
    private Action ExecuteLocationEvent(Location destination) {
        LocationSD currentLocationSD = Managers.Player.PlayerData.CurrentLocationID.ToLocationSD();
        LocationSD endLocationSD = destination.LocationSD;
        if (currentLocationSD.Equals(endLocationSD)) {
            return () => EnterLocation(destination);
        }
        else {
            return () => MoveLocation(currentLocationSD, endLocationSD);
        }
    }
    private void EnterLocation(Location destination) {
        Managers.UI.CloseUI<LocationInfoPopUpUI>();
        var ui = Managers.UI.OpenUI<ExplorationUI>();
        ui.InitLocationUI(destination);
        ui.ShowEnterance();
    }
    private void MoveLocation(LocationSD currentLocationSD, LocationSD endLocationSD) {
        Managers.UI.CloseUI<LocationInfoPopUpUI>();
        Managers.UI.GetUI<MapUI>().LocationPointer.MovePosition(
            currentLocationSD,
            endLocationSD,
            callback: () => {
                Managers.Player.PlayerData.SetCurrentLocation(endLocationSD);
                if (Managers.Location.TryGetLocation(endLocationSD, out var destination)) {
                    InitPopUp(destination);
                    Managers.UI.OpenUI(this);
                }
            });
    }
    #endregion
}
