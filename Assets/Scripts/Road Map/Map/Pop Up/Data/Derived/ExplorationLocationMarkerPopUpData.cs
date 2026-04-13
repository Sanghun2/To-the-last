using System;
using UnityEngine;

public class ExplorationLocationMarkerPopUpData : MarkerPopUpDataBase,
    IProgressContent
{
    public float CurrentProgress { get; }
    public float MaxProgress { get; }


    public ExplorationLocationMarkerPopUpData(
        LocationBase location,
        ActionData[] buttonActions, 
        Action onCloseByPanel = null)
        : base(location, buttonActions, onCloseByPanel) {

        var exp = location as ExplorationLocation;
        CurrentProgress = exp.CurrentValue;
        MaxProgress = exp.MaxValue;
    }
}
