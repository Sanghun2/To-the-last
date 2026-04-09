using System.Collections.Generic;
using UnityEngine;

public class SelectionData : SelectionDataBase
{
    public int RequireMinutes { get; }

    public SelectionData(SelectionSD selectionSD) 
        : base(selectionSD) {

        RequireMinutes = selectionSD.RequireMinutes;
    }
}
