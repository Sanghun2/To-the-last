using System;
using UnityEngine;

public abstract class ProductionContextProcessorBase
{
    public abstract bool TryCraft(ProductionContextBase contentContext, ProductionContentUI targetUI);
}

public abstract class ProductionContextProcessorBase<TContext> : ProductionContextProcessorBase
{
    public override bool TryCraft(ProductionContextBase contentContext, ProductionContentUI targetUI) {
        if (contentContext is TContext context) {
            return TryProcessContext(context, targetUI);
        }

        Debug.LogError($"<color=red>({contentContext.GetType()}) is not type of ({typeof(TContext)})</color>");
        return false;
    }

    public abstract bool TryProcessContext(TContext contentContext, ProductionContentUI targetUI);
}
