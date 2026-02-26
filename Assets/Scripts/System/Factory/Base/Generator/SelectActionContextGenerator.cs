using System;
using UnityEngine;

public abstract class SelectActionContextGenerator
{
    public abstract bool TryGenerateContext(SelectionSD selectionSD, out SelectActionContext context);
}
