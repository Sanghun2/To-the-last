using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUpDataBase
{
    public IReadOnlyList<ActionData> ButtonActions => buttonActions;
    public Action OnCloseByPanel => onCloseByPanel;


    protected ActionData[] buttonActions;
    protected Action onCloseByPanel;

    public PopUpDataBase(ActionData[] buttonActions, Action onCloseByPanel=null) {
        this.buttonActions = buttonActions;
        this.onCloseByPanel = onCloseByPanel;
    }
}
