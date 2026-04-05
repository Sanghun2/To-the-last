using System.Linq;
using UnityEngine;

public class DelayedProductionDataParser : ProductionDataParserBase<DelayedProductionContentSD, DelayedProductionData>
{
    public override DelayedProductionData ParseData(DelayedProductionContentSD contentSD) {
        return new DelayedProductionData(
            contentSD.ID,
            contentSD.Outputs.First()?.Amount ?? 0,
            contentSD.RequireMinutesToComplete
            );
    }
}
