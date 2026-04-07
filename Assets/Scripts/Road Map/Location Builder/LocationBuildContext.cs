using UnityEngine;

public class LocationBuildContext
{
    public string LocationUID { get; }
    public string LocationCategoryID { get; }
    public Vector2 AnchoredPosition { get; }
    public string DisplayName { get; }

    public LocationBuildContext(
        string locationUID, 
        string locationCategoryID, 
        string displayName,
        Vector2 locationCoordinate) {

        LocationUID = locationUID;
        LocationCategoryID = locationCategoryID;
        DisplayName = displayName;
        AnchoredPosition = locationCoordinate;
    }
}
