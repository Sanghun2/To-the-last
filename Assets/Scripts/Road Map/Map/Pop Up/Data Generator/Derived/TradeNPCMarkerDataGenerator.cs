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
}
