using UnityEngine;

public class LocationInfoPopUpData : PopUpData, IImageContent
{
    public ExplorationLocation Location => location;
    public Sprite MainImage => location.Data.MainImage;

    [SerializeField] ExplorationLocation location;


    public LocationInfoPopUpData(string title, string description, ActionData[] buttonActions) : base(title, description, buttonActions) {
    }


    public LocationInfoPopUpData(ExplorationLocation location, ActionData[] buttonActions)
        : base(location.LocationName, location.StoryDescription, buttonActions) {
        this.location = location;
    }
}
