using UnityEngine;

public abstract class SelectActionContextBuilderBase
{
    public abstract SelectActionContextBase BuildActionContext(SelectionRunnerDataBase selectionData);
}

public abstract class SelectActionContextBuilderBase<TRunnerData, TContext> : SelectActionContextBuilderBase
    where TRunnerData : SelectionRunnerDataBase
    where TContext : SelectActionContextBase
{
    public abstract TContext BuildActionContext(TRunnerData data);

    public override SelectActionContextBase BuildActionContext(SelectionRunnerDataBase selectionData) {
        if (selectionData is TRunnerData convertedData) {
            return BuildActionContext(convertedData);
        }

        Debug.LogError($"<color=red>({selectionData.GetType()}) is not type of ({typeof(TRunnerData)})</color>");
        return null;
    }
}