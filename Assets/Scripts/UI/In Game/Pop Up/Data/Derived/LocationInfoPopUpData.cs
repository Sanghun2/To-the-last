using UnityEngine;

public class LocationInfoPopUpData : PopUpData, IImageContent
{
    public Location Location => location;
    public Sprite Image => location.Data.MainImage;

    [SerializeField] Location location;


    public LocationInfoPopUpData(string title, string description, ActionData[] buttonActions) : base(title, description, buttonActions) {
    }


    public LocationInfoPopUpData(Location location, ActionData[] buttonActions)
        : base(location.TitleText, location.StoryDescription, buttonActions) {
        this.location = location;
    }
}
