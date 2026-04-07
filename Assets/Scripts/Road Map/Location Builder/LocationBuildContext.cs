using UnityEngine;

public class LocationBuildContext
{
    public string LocationID { get; }
    public Vector2 AnchoredPosition { get; }

    public LocationBuildContext(string locationID, Vector2 locationCoordinate) {
        LocationID = locationID;
        AnchoredPosition = locationCoordinate;
    }
}
