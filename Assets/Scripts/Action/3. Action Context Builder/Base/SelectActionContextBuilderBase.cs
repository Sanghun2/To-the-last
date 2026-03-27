using UnityEngine;

public abstract class SelectActionContextBuilderBase
{
    public abstract bool TryBuildActionContext(SelectionDataBase selectionData, out SelectActionContextBase context);
}

public abstract class SelectActionContextBuilderBase<TData, TContext> : SelectActionContextBuilderBase
    where TData : SelectionDataBase
    where TContext : SelectActionContextBase
{
    public abstract bool TryBuildActionContext(TData data, out TContext context);

    public override bool TryBuildActionContext(SelectionDataBase selectionData, out SelectActionContextBase context) {
        if (selectionData is TData convertedData) {
            var result = TryBuildActionContext(convertedData, out TContext tContext);
            context = tContext;
            return result;
        }

        Debug.LogError($"<color=red>({selectionData.GetType()}) is not type of ({typeof(TData)})</color>");
        context = null;
        return false;
    }
}