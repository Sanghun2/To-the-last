using UnityEngine;

public class TradeNPCMarkerDataGenerator : MarkerDataGeneratorBase<TradeNPCLocation, TradeNPCMarkerPopUpData>
{
    public override TradeNPCMarkerPopUpData GenerateData(TradeNPCLocation location) {
        return new TradeNPCMarkerPopUpData(
            null
            );
    }
}
