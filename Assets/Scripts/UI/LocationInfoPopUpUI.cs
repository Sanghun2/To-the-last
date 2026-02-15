using UnityEngine;
using UnityEngine.UI;


public class LocationInfoPopUpData : InfomationPopUpData
{
    public Sprite LocationImage => locationImage;

    [SerializeField] Sprite locationImage;
    public LocationInfoPopUpData(string title, Sprite locationImage, string description, ActionData[] buttonActions, string subText = null, Sprite image = null) : base(title, description, buttonActions, subText, image) {
        this.locationImage = locationImage;
    }
}

public class LocationInfoPopUpUI : PopUpUIBase<LocationInfoPopUpData>
{
    [SerializeField] protected Image locationImage;

    public override void InitUI(LocationInfoPopUpData popUpData) {
        base.InitUI(popUpData);
        locationImage.sprite = popUpData.LocationImage;
    }
}
