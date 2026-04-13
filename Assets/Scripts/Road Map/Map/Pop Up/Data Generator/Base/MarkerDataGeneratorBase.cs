using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public abstract class MarkerDataGeneratorBase
{
    public abstract MarkerPopUpDataBase GenerateData(LocationBase location);

}

public abstract class MarkerDataGeneratorBase<TLocation, TMarkerData> : MarkerDataGeneratorBase
    where TLocation : LocationBase
    where TMarkerData : MarkerPopUpDataBase
{
    public override MarkerPopUpDataBase GenerateData(LocationBase location) {
        if (location is TLocation tLocation) {
            return GenerateData(tLocation);
        }

        Debug.LogError($"<color=red>({location.GetType()}) is not type of ({typeof(TLocation)})</color>");
        return null;
    }

    public abstract TMarkerData GenerateData(TLocation location);


    protected ActionData[] GetButtonAction(TLocation location) {
        return new ActionData[]{
            new ActionData("확인", () => Managers.UI.CloseTopUI()),
            new ActionData(GetButtonText(location), GetConfirmEvent(location))
        };
    }

    private Action GetConfirmEvent(TLocation destination) {
        LocationData currentLocationData = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID)?.Data;
        LocationData endLocationData = destination.Data;
        if (currentLocationData.Equals(endLocationData)) {
            return () => EnterLocation(destination);
        }
        else {
            return () => MoveLocation(currentLocationData, endLocationData);
        }
    }

    private string GetButtonText(TLocation destination) {
        LocationBase currentSD = LocationUtility.FindLocation(Managers.Player.PlayerData.CurrentLocationID);
        LocationBase destinationSD = destination;
        if (currentSD.Equals(destinationSD)) {
            return "들어간다";
        }

        return "이동한다";
    }
    private void EnterLocation(LocationBase destination) {
        Managers.UI.CloseUI<MarkerInfoPopUpUI>();

        // basement
        if (destination.LocationUID.Equals(Define.Tag.BASEMENT)) {
            var basementUI = Managers.UI.GetUI<BasementUI>();
            Managers.UI.CloseUI<MapUI>();
            basementUI.OpenUI();
        }
        // trade npc
        else if (false) { }
        // exploration
        else {
            var ui = Managers.UI.GetUI<ExplorationUI>();
            ui.InitUI();
            ui.OpenUI();

            ui.InitLocationUI(destination as ExplorationLocation);
            ui.ShowEnterance();
        }
    }
    private void MoveLocation(LocationData currentLocationData, LocationData endLocationData) {
        Managers.UI.CloseUI<MarkerInfoPopUpUI>();
        Managers.UI.GetUI<MapUI>().LocationPointer.MovePosition(
            currentLocationData,
            endLocationData,
            callback: () => {
                if (Managers.Location.TryGetLocation(endLocationData.LocationUID, out var destination)) {
                    Managers.Location.CurrentLocation = destination;
                    var popUpUI = Managers.UI.GetUI<MarkerInfoPopUpUI>();
                    popUpUI.InitPopUp(Managers.Location.GenerateMarkerData(destination));
                    Managers.UI.OpenUI(popUpUI);
                }
            });
    }
}
