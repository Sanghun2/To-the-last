using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionData : ActionData
{
    public SelectionData(Action action) : base(action) {
    }
    public SelectionData(string text, Action action) : base(text, action) {
    }
    public Define.RequirementType RequirementType => requirementType;
    public IReadOnlyList<Ingredient> Requirements => requirements;

    protected Ingredient[] requirements;
    protected Define.RequirementType requirementType;

    public SelectionData SetRequirements(Ingredient[] requirements) {
        this.requirements = requirements;
        return this;
    }
    public SelectionData SetSelectionType(Define.RequirementType type) {
        requirementType = type;
        return this;
    }
}
