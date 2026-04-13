using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationData : IEquatable<LocationData>
{
    public string LocationUID { get; }
    public IReadOnlyList<EncounterDataBase> LocationEventList { get; }
    public string StoryDescription { get; }
    public Vector2 AnchoredPosition { get; }
    public Sprite MainImage { get; }
    public Sprite IconImage { get; }
    public string DisplayName { get; }
    public string NextLocationID { get; }
    public string LocationCategoryID { get; }

    public LocationData(
        string uid, 
        string categoryID,
        IReadOnlyList<EncounterDataBase> locationEventList, 
        string displayText,
        string storyDescription, 
        Vector2 anchoredPosition,
        Sprite mainImage,
        Sprite iconImage,
        string nextLocationID) {

        LocationUID = uid;
        LocationCategoryID = categoryID;
        LocationEventList = locationEventList;
        StoryDescription = storyDescription;
        AnchoredPosition = anchoredPosition;
        MainImage = mainImage;
        IconImage = iconImage;
        DisplayName = displayText;
        NextLocationID = nextLocationID;
    }

    public LocationData(CoordinateData coordinate) {
        LocationUID = coordinate.LocationUID;
        AnchoredPosition = coordinate.AnchoredPosition;
    }

    public bool Equals(LocationData other) {
        if (this == null || other == null) return false;

        return LocationUID.Equals(other.LocationUID);
    }
}
