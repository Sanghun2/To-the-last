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

    public string NextLocationID => nextLocationID;

    private IReadOnlyList<EncounterEvent> locationEventList;
    private string storyDescription;
    private Vector2 anchoredPosition;
    private string locationID;
    private Sprite mainImage;
    private Sprite iconImage;
    private string displayText;
    private string nextLocationID;

    public LocationData(
        string id, 
        IReadOnlyList<EncounterEvent> locationEventList, 
        string displayText,
        string storyDescription, 
        Vector2 anchoredPosition,
        Sprite mainImage,
        Sprite iconImage,
        string nextLocationID) {

        this.locationID = id;
        this.locationEventList = locationEventList;
        this.storyDescription = storyDescription;
        this.anchoredPosition = anchoredPosition;
        this.mainImage = mainImage;
        this.iconImage = iconImage;
        this.displayText = displayText;
        this.nextLocationID = nextLocationID;
    }

    public LocationData(CoordinateData coordinate) {
        locationID = coordinate.LocationUID;
        anchoredPosition = coordinate.LocationCoordinate;
    }

    public bool Equals(LocationData other) {
        if (this == null || other == null) return false;

        return locationID.Equals(other.locationID);
    }
}
