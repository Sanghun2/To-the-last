using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectActionData : ActionData
{
    public SelectionContext SelectionContext => _selectionContext;

    private SelectionContext _selectionContext;

    public SelectActionData(SelectionContext selectionContext) 
        : base(selectionContext.SelectAction) {
        this._selectionContext = selectionContext;
    }
}
