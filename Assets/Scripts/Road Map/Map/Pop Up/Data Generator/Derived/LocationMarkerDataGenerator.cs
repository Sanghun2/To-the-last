using System;
using UnityEngine;

public class LocationMarkerDataGenerator : MarkerDataGeneratorBase<ExplorationLocation, ExplorationLocationMarkerPopUpData>
{
    public override ExplorationLocationMarkerPopUpData GenerateData(ExplorationLocation location) {
        return new ExplorationLocationMarkerPopUpData(
            location,
            GetButtonAction(location)
            );
    }

    protected override void ExecuteEnter(LocationBase destination) {
        var ui = Managers.UI.GetUI<ExplorationUI>();
        ui.InitUI();
        ui.OpenUI();

        ui.InitLocationUI(destination);
        ui.ShowEnterance();
    }
}
