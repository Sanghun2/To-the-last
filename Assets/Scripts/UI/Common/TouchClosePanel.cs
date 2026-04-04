using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchClosePanel : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    protected Action onClosed;

    public void SetCloseAction(Action onClosed) {
        this.onClosed = onClosed;
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData) {
        Managers.UI.CloseTopUI();
        onClosed?.Invoke();
        onClosed = null;
    }
}
