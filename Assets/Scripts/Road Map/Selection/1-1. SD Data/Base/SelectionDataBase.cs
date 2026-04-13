using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectionDataBase
{
    public IReadOnlyList<Condition> UnlockConditions { get; }
    public Condition ConditionToSelect { get; }
    public string Description { get; }
    public int RequireMinutes { get; }

    public SelectionDataBase(SelectionSD sd) {
        UnlockConditions = sd.UnlockConditions;
        ConditionToSelect = sd.ConditionToSelect;
        Description = sd.DisplayName;
        RequireMinutes = sd.RequireMinutes;
    }
}
