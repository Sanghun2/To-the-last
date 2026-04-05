using System;
using UnityEngine;

public abstract class ProductionContextBuilderBase
{
    public abstract ProductionContextBase BuildContext(ProductionDataBase contentData);
}

public abstract class ProductionContextBuilderBase<TData, TContext> : ProductionContextBuilderBase
    where TData : ProductionDataBase
    where TContext : ProductionContextBase
{
    public override ProductionContextBase BuildContext(ProductionDataBase contentData) {
        if (contentData is TData data) {
            return BuildContext(data);
        }

        Debug.LogError($"<color=red>({contentData.GetType()}) failed to build context ({typeof(TContext)})</color>");
        return null;
    }

    public abstract TContext BuildContext(TData contentData);
}
