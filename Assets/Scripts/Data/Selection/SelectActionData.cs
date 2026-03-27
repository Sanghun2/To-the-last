using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectActionData : ActionData
{
    public Define.RequirementType RequirementType => SelectionData.RequirementType;
    public Ingredient Requirement => SelectionData.Requirement;

    public bool IsLocked => locked;
    private SelectionDataBase SelectionData
    {
        get => _selectionContext.SelectionData;
    }


    private SelectionContextBase _selectionContext;
    private bool locked;

    public SelectActionData(SelectionContextBase selectionContext) : base(selectionContext.SelectionData.DisplayText, selectionContext.ActionData.Action) {
        this._selectionContext = selectionContext;
    }
}
