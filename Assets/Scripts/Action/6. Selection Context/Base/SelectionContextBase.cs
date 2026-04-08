using System;
using UnityEngine;

public abstract class SelectionContextBase
{
    public SelectionRunnerDataBase SelectionData => selectionData;
    public ActionData ActionData => actionData;

    protected ActionData actionData;
    protected SelectionRunnerDataBase selectionData;
}

public abstract class SelectionContextBase<TData> : SelectionContextBase
    where TData : SelectionRunnerDataBase
{
    public new TData SelectionData => (TData)selectionData;
}
