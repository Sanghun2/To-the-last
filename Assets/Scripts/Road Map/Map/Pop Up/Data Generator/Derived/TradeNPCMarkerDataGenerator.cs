using UnityEngine;

public class TradeNPCMarkerDataGenerator : MarkerDataGeneratorBase<TradeNPCLocation, TradeNPCMarkerPopUpData>
{
    public override TradeNPCMarkerPopUpData GenerateData(TradeNPCLocation location) {
        return new TradeNPCMarkerPopUpData(
            location,
            location.TradeNPC.CurrentAffinity,
            location.TradeNPC.MaxAffinity,
            GetButtonAction(location)
            );
    }

    protected override void ExecuteEnter(LocationBase destination) {
        var ui = Managers.UI.GetUI<TradeUI>();
        ui.InitUI();
        ui.InitLocationUI(destination);
        ui.ShowEnterance();
        ui.OpenUI();
    }
}
