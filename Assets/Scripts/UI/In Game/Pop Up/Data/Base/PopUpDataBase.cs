using System.Collections.Generic;
using UnityEngine;

public abstract class PopUpDataBase
{
    public IReadOnlyList<ActionData> ButtonActions => buttonActions;
    protected ActionData[] buttonActions;

    public PopUpDataBase(ActionData[] buttonActions) {
        this.buttonActions = buttonActions;
    }
}
