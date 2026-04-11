using UnityEngine;
using UnityEngine.EventSystems;

public class SkipDialogPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] DialogUIBase dialogUI;

    public void OnPointerDown(PointerEventData _) {
    }

    public void OnPointerUp(PointerEventData _) {
        var dialog = Managers.Dialog.CurrentDialog;
        var page = dialog.CurrentPage;
        Debug.Log("touched");
        if (dialogUI.CurrentPageState == DialogUIBase.PageState.InProgress) {
            dialogUI.SkipDialogAnimation();
        }
        else if (dialogUI.CurrentPageState == DialogUIBase.PageState.Completed) {
            dialog.JumpToNextPage();
        }
    }
}
