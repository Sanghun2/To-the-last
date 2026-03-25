using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationData : IEquatable<LocationData>
{
    public string LocationID => locationID;
    public IReadOnlyList<EncounterEvent> LocationEventList => locationEventList;
    public string StoryDescription => storyDescription;
    public Vector2 AnchoredPosition => anchoredPosition;
    public Sprite MainImage => mainImage;
    public Sprite IconImage => iconImage;
    public string DisplayText => displayText;


    private IReadOnlyList<EncounterEvent> locationEventList;
    private string storyDescription;
    private Vector2 anchoredPosition;
    private string locationID;
    private Sprite mainImage;
    private Sprite iconImage;
    private string displayText;

    public LocationData(
        string iD, 
        IReadOnlyList<EncounterEvent> locationEventList, 
        string displayText,
        string storyDescription, 
        Vector2 anchoredPosition,
        Sprite mainImage,
        Sprite iconImage) {

        this.locationEventList = locationEventList;
        this.storyDescription = storyDescription;
        this.anchoredPosition = anchoredPosition;
        this.mainImage = mainImage;
        this.iconImage = iconImage;
        this.displayText = displayText;
    }

    public bool Equals(LocationData other) {
        if (this == null || other == null) return false;

        return locationID.Equals(other.locationID);
    }
}
