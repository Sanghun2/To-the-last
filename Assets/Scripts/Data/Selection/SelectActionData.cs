using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectActionData : ActionData
{
    public Define.RequirementType RequirementType => selectionSD.RequirementType;
    public Ingredient Requirement => selectionSD.Requirement;
    public SelectionSD SelectionSD => selectionSD;
    public bool IsLocked => locked;


    protected SelectionSD selectionSD;
    private bool locked;

    public SelectActionData(Action action) : base(action) {
    }
    public SelectActionData(string text, Action action) : base(text, action) {
    }
    public SelectActionData(SelectionSD selectionSD, Action action) : base(selectionSD.DisplayText, action) {
        this.selectionSD = selectionSD;
    }
}
