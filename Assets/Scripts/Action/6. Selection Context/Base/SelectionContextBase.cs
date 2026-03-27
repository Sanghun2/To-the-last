using System;
using UnityEngine;

public abstract class SelectionContextBase
{
    public SelectionDataBase SelectionData => selectionData;
    public ActionData ActionData => actionData;

    protected ActionData actionData;
    protected SelectionDataBase selectionData;
}

public abstract class SelectionContextBase<TData> : SelectionContextBase
    where TData : SelectionDataBase
{
    public new TData SelectionData => (TData)selectionData;
}
