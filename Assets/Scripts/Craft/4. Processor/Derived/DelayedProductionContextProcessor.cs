using UnityEngine;

public class DelayedProductionContextProcessor : ProductionContextProcessorBase<DelayedProductionContext>
{
    public override bool TryProcessContext(DelayedProductionContext contentContext, ProductionContentUI targetUI) {
        if (Managers.Craft.TryRegisterDelayedProduction(contentContext, targetUI)) {
            return true;
        }

        Debug.LogError($"<color=red>failed to process ({contentContext.GetType()})</color>");
        return false;
    }
}
