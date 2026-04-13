using System;
using UnityEngine;

public abstract class MarkerPopUpDataBase : PopUpDataBase
{
    protected MarkerPopUpDataBase(ActionData[] buttonActions, Action onCloseByPanel = null) : base(buttonActions, onCloseByPanel) {
    }
}
