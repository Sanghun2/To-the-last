using System;
using UnityEngine;

public abstract class MarkerUIContainerBase : ListContainerBase<MarkerUIBase>
{
    public abstract Type MarkerUIType { get; }
}

public abstract class MarkerUIContainerBase<TMarkderUI> : MarkerUIContainerBase
    where TMarkderUI : MarkerUIBase
{
    public override Type MarkerUIType => typeof(TMarkderUI);

    public new TMarkderUI GetObj() {
        return (TMarkderUI)base.GetObj();
    }
}
