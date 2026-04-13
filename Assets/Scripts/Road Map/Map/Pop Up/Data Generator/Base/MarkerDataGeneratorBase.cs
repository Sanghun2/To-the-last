using System;
using UnityEngine;

public abstract class MarkerDataGeneratorBase
{
    public abstract MarkerPopUpDataBase GenerateData(LocationBase location);
}

public abstract class MarkerDataGeneratorBase<TLocation, TMarkerData> : MarkerDataGeneratorBase
    where TLocation : LocationBase
    where TMarkerData : MarkerPopUpDataBase
{
    public override MarkerPopUpDataBase GenerateData(LocationBase location) {
        if (location is TLocation tLocation) {
            return GenerateData(tLocation);
        }

        Debug.LogError($"<color=red>({location.GetType()}) is not type of ({typeof(TLocation)})</color>");
        return null;
    }

    public abstract TMarkerData GenerateData(TLocation location);
}
