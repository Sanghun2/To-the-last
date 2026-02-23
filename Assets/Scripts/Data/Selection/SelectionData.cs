using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionData : ActionData
{

    public Define.RequirementType RequirementType => requirementType;
    public Ingredient Requirement => requirement;

    protected Ingredient requirement;
    protected Define.RequirementType requirementType;


    public SelectionData(Action action) : base(action) {
    }
    public SelectionData(string text, Action action) : base(text, action) {
    }
    public SelectionData(SelectionSD selectionSD) : base(selectionSD.Description, null){
        SetRequirement(selectionSD.Requirement);
        SetSelectionType(selectionSD.RequirementType);
    }


    public SelectionData SetRequirement(Ingredient requirement) {
        this.requirement = requirement;
        return this;
    }
    public SelectionData SetSelectionType(Define.RequirementType type) {
        requirementType = type;
        return this;
    }
}
