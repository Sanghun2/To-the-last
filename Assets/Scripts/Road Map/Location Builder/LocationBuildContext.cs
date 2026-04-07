using UnityEngine;

public class LocationBuildContext
{
    public string LocationUID { get; }
    public string LocationCategoryID { get; }
    public Vector2 AnchoredPosition { get; }

    public LocationBuildContext(string locationUID, string locationCategoryID, Vector2 locationCoordinate) {
        LocationUID = locationUID;
        LocationCategoryID = locationCategoryID;
        AnchoredPosition = locationCoordinate;
    }
}
