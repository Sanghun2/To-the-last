using System;
using UnityEngine;

public abstract class LocationBase : IEquatable<LocationBase>
{
    public string LocationUID => data.LocationUID;
    public LocationData Data => data;

    private LocationData data;

    public LocationBase(LocationData data) {
        this.data = data;
    }

    public bool Equals(LocationBase other) {
        if (this == null || other == null) return false;

        return LocationUID.Equals(other.LocationUID);
    }
}
