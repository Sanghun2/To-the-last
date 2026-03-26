using System;
using UnityEngine;

public abstract class SelectActionContextBuilderBase
{
    public abstract bool TryBuildContext(SelectionDataBase selectionData, out SelectActionContext context);
}
