using System;
using UnityEngine;

public abstract class MarkerPopUpDataBase : PopUpDataBase,
    IImageContent, ITitleContent, IDescriptionContent
{
    public LocationBase Location { get; }

    public string Title { get; }
    public string Description { get; }
    public Sprite MainImage { get; }



    protected MarkerPopUpDataBase(LocationBase location, ActionData[] buttonActions, Action onCloseByPanel = null) : base(buttonActions, onCloseByPanel) {
        Location = location;
        Title = location.LocationName;
        Description = location.StoryDescription;
        MainImage = location.MainImage;
    }
}
