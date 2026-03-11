using UnityEngine;
using UnityEngine.EventSystems;

public class TouchClosePanel : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData) {
        Managers.UI.CloseTopUI();
    }
}
