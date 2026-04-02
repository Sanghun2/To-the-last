using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;


public class LocationInfoPopUpData : PopUpData
{
    public Location Location => location;

    [SerializeField] Location location;

    public LocationInfoPopUpData(string title, string description, ActionData[] buttonActions) : base(title, description, buttonActions) {
    }


    public LocationInfoPopUpData(Location location, ActionData[] buttonActions) 
        : base (location.Data.DisplayText, location.Data.StoryDescription, buttonActions){
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
        var sd = popUpData.Location.Data;
        locationImage.sprite = sd.MainImage;

        int currentProgress = popUpData.Location.CurrentValue;
        int maxProgress = sd.LocationEventList.Count;
        InitProgressUI(currentProgress, maxProgress);

        Location destination = popUpData.Location;
        Location currentLocation = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID);
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
    private void InitMoveTimeUI(Location currentLocation, Location destination) {
        bool isSamePosition = currentLocation.Equals(destination);
        moveTimeExpectationText.gameObject.SetActive(!isSamePosition);
        if (isSamePosition) return;

        var time = LocationUtility.CalculateDistance(currentLocation.Data.AnchoredPosition, destination.Data.AnchoredPosition).ConvertToTime();
        moveTimeExpectationText.SetText($"{time.hour}시간 {time.minutes}분");
        //Debug.Log($"{currentLocation.TraitID} -> {destination.TraitID}");
    }


    #region UI Info
    private string GetButtonText(Location destination) {
        Location currentSD = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID);
        Location destinationSD = destination;
        if (currentSD.Equals(destinationSD)) {
            return "들어간다";
        }

        return "이동한다";
    }
    private Action ExecuteLocationEvent(Location destination) {
        LocationData currentLocationData = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID)?.Data;
        LocationData endLocationData = destination.Data;
        if (currentLocationData.Equals(endLocationData)) {
            return () => EnterLocation(destination);
        }
        else {
            return () => MoveLocation(currentLocationData, endLocationData);
        }
    }
    private void EnterLocation(Location destination) {
        Managers.UI.CloseUI<LocationInfoPopUpUI>();
        var ui = Managers.UI.OpenUI<ExplorationUI>();
        ui.InitLocationUI(destination);
        ui.ShowEnterance();
    }
    private void MoveLocation(LocationData currentLocationData, LocationData endLocationData) {
        Managers.UI.CloseUI<LocationInfoPopUpUI>();
        Managers.UI.GetUI<MapUI>().LocationPointer.MovePosition(
            currentLocationData,
            endLocationData,
            callback: () => {
                Managers.Player.PlayerData.SetCurrentLocation(endLocationData);
                if (Managers.Location.TryGetLocation(endLocationData.LocationID, out var destination)) {
                    InitPopUp(destination);
                    Managers.UI.OpenUI(this);
                }
            });
    }
    #endregion
}
