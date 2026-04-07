using System;
using UnityEngine;

public abstract class ToastPresenterBase
{
    public abstract void PresentToast(ToastMessageUI toastMessageUI, Vector2 endPos, float viewDuration);
}
