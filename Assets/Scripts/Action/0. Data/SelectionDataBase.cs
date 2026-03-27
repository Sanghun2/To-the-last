using System;
using UnityEngine;

public abstract class SelectionDataBase
{
    public int RequireMinutes => requireMinutes;
    public string DisplayText => displayText;
    public Define.RequirementType RequirementType => requirementType;
    public Ingredient Requirement => requirement;

    protected int requireMinutes;
    protected string displayText;
    protected Define.RequirementType requirementType;
    protected Ingredient requirement;

    public SelectionDataBase(int requireMinutes, string displayText, Define.RequirementType requirementType, Ingredient requirement) {
        this.requireMinutes = requireMinutes;
        this.displayText = displayText;
        this.requirementType = requirementType;
        this.requirement = requirement;
    }
}
