using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationData : IEquatable<LocationData>
{
    public string LocationUID { get; }
    public string LocationCategoryID { get; }
    public string DisplayName { get; }
    public Sprite MainImage { get; }
    public Sprite IconImage { get; }
    public string StoryDescription { get; }
    public Vector2 AnchoredPosition { get; }

    public LocationData(
        string uid, 
        string categoryID,
        string displayName,
        string storyDescription, 
        Vector2 anchoredPosition,
        Sprite mainImage,
        Sprite iconImage
        ) {

        LocationUID = uid;
        LocationCategoryID = categoryID;
        DisplayName = displayName;
        StoryDescription = storyDescription;
        AnchoredPosition = anchoredPosition;
        MainImage = mainImage;
        IconImage = iconImage;
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
