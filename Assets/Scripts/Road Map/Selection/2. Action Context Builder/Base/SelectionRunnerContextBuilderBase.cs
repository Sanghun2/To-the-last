using UnityEngine;

public abstract class SelectionRunnerContextBuilderBase
{
    public abstract SelectionRunnerContextBase BuildSelectionRunnerContext(SelectionRunnerDataBase selectionRunnerData);
}

public abstract class SelectionRunnerContextBuilderBase<TRunnerData, TContext> : SelectionRunnerContextBuilderBase
    where TRunnerData : SelectionRunnerDataBase
    where TContext : SelectionRunnerContextBase
{
    public abstract TContext BuildActionContext(TRunnerData data);

    public override SelectionRunnerContextBase BuildSelectionRunnerContext(SelectionRunnerDataBase selectionRunnerData) {
        if (selectionRunnerData is TRunnerData runnerData) {
            return BuildActionContext(runnerData);
        }

        Debug.LogError($"<color=red>({selectionRunnerData.GetType()}) is not type of ({typeof(TRunnerData)})</color>");
        return null;
    }
}