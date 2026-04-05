using UnityEngine;

public class DelayedProductionContextBuilder : ProductionContextBuilderBase<DelayedProductionData, DelayedProductionContext>
{
    public override DelayedProductionContext BuildContext(DelayedProductionData contentData) {
        return new DelayedProductionContext(
            contentData.ID,
            contentData.Amount,
            contentData.RequireMinutesToComplete
            );
    }
}
