using System;
using UnityEngine;

public class TradeNPCMarkerPopUpData : MarkerPopUpDataBase, IAffinityContent
{
    public float MaxAffinity { get; }
    public float CurrentAffinity { get; }


    public TradeNPCMarkerPopUpData(
        LocationBase location, 
        float currentAffinity,
        float maxAffinity,
        ActionData[] buttonActions, 
        Action onCloseByPanel = null) 
        : base(location, buttonActions, onCloseByPanel) {

        CurrentAffinity = currentAffinity;
        MaxAffinity = maxAffinity;
    }
}
