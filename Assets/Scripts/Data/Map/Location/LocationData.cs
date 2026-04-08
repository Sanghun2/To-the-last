using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationData : IEquatable<LocationData>
{
    public string LocationUID => locationUID;
    public IReadOnlyList<EncounterInfo> LocationEventList => locationEventList;
    public string StoryDescription => storyDescription;
    public Vector2 AnchoredPosition => anchoredPosition;
    public Sprite MainImage => mainImage;
    public Sprite IconImage => iconImage;
    public string DisplayText => displayText;
    public string NextLocationID => nextLocationID;
    public string LocationCategoryID => locationCategoryID;



    private IReadOnlyList<EncounterInfo> locationEventList;
    private string storyDescription;
    private Vector2 anchoredPosition;
    private string locationUID;
    private string locationCategoryID;
    private Sprite mainImage;
    private Sprite iconImage;
    private string displayText;
    private string nextLocationID;

    public LocationData(
        string uid, 
        string categoryID,
        IReadOnlyList<EncounterInfo> locationEventList, 
        string displayText,
        string storyDescription, 
        Vector2 anchoredPosition,
        Sprite mainImage,
        Sprite iconImage,
        string nextLocationID) {

        this.locationUID = uid;
        this.locationCategoryID = categoryID;
        this.locationEventList = locationEventList;
        this.storyDescription = storyDescription;
        this.anchoredPosition = anchoredPosition;
        this.mainImage = mainImage;
        this.iconImage = iconImage;
        this.displayText = displayText;
        this.nextLocationID = nextLocationID;
    }

    public LocationData(CoordinateData coordinate) {
        locationUID = coordinate.LocationUID;
        anchoredPosition = coordinate.AnchoredPosition;
    }

    public bool Equals(LocationData other) {
        if (this == null || other == null) return false;

        return locationUID.Equals(other.locationUID);
    }
}
