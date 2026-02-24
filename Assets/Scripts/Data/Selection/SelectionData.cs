using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionData : ActionData
{
    public Define.RequirementType RequirementType => selectionSD.RequirementType;
    public Ingredient Requirement => selectionSD.Requirement;
    public SelectionSD SelectionSD => selectionSD;  

    protected SelectionSD selectionSD;


    public SelectionData(Action action) : base(action) {
    }
    public SelectionData(string text, Action action) : base(text, action) {
    }
    public SelectionData(SelectionSD selectionSD, Action action) : base(selectionSD.DisplayName, action) {
        this.selectionSD = selectionSD;
    }
}
