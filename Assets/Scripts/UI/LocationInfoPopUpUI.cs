using UnityEngine;
using UnityEngine.UI;


public class LocationInfoPopUpData : PopUpData
{
    public Sprite LocationImage => locationImage;

    [SerializeField] Sprite locationImage;

    public LocationInfoPopUpData(string title, string description, ActionData[] buttonActions) : base(title, description, buttonActions) {
    }


    public LocationInfoPopUpData(LocationSD locationSD, ActionData[] buttonActions) : base (locationSD.DisplayName, locationSD.StoryDescription, buttonActions){
        locationImage = locationSD.MainImage;
    }
}

public class LocationInfoPopUpUI : PopUpUIBase<LocationInfoPopUpData>
{
    [SerializeField] protected Image locationImage;

    public override void InitPopUp(LocationInfoPopUpData popUpData) {
        base.InitPopUp(popUpData);
        locationImage.sprite = popUpData.LocationImage;
    }
}
