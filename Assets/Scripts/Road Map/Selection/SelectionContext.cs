using System;
using UnityEngine;

public sealed class SelectionContext
{
    public string Description { get; }
    public Define.RequirementType RequirementType { get; }
    public Requirement Requirement { get; }
    public bool IsLocked { get; }
    public Action SelectAction { get; }

    public SelectionDataBase SelectionData { get; }
    public ActionData SelectActionData { get; }
    public int RequireMinutes { get; }

    public SelectionContext(SelectionDataBase selectionData, ActionData selectActionData) {
        SelectionData = selectionData;
        SelectActionData = selectActionData;

        Description = selectionData.Description;

        var requrementInfo = selectionData.ConditionToSelect;
        if (requrementInfo != null && requrementInfo.RequirementType != Define.RequirementType.Free) {
            if (requrementInfo.RequiredTarget != null) {
                RequirementType = requrementInfo.RequirementType;
                Requirement = new Requirement(requrementInfo.RequiredTarget.Image, requrementInfo.RequirementAmount);
            }
            else {
                Debug.LogError($"<color=red>required target is null</color>");
            }
        }
        IsLocked = false;
        SelectAction = selectActionData.Action;
        RequireMinutes = selectionData.RequireMinutes;
    }
}
