using UnityEngine;
using UnityEngine.EventSystems;

public class SkipDialogPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] DialogUIBase dialogUI;

    public void OnPointerDown(PointerEventData _) {
    }

    public void OnPointerUp(PointerEventData _) {
        dialogUI.SkipDialogAnimation();
    }
}
