using System;
using DG.Tweening;
using UnityEngine;

public class Toast
{
    public enum Type {
        None,
        Info,
        Warning,
        Error,
        Confirm,
    }
}

public sealed class ToastManager : MonoBehaviour
{
    private ToastMessageContainer ToastContainer
    {
        get
        {
            if (_toastContainer == null) {
                _toastContainer = GameObject.FindAnyObjectByType<ToastMessageContainer>(FindObjectsInactive.Include);
                if (_toastContainer == null) { Debug.LogError($"<color=red>toast container를 찾을 수 없음 동적 생성 구현 필요</color>"); }
            }

            return _toastContainer;
        }
    }
    private RectTransform Rect
    {
        get
        {
            if (_rect == null) {
                _rect = GetComponent<RectTransform>();  
            }

            return _rect;
        }
    }

    [SerializeField] ToastMessageContainer _toastContainer;
    [SerializeField] RectTransform toastDestination;
    private RectTransform _rect;
    //private ToastPresenterBase toastPresenter = new DotweenToastPresenter(Ease.OutFlash, Ease.Unset);

    public void SetPresenter(ToastPresenterBase presenter) {
        //toastPresenter = presenter;
    }

    public void ShowToast(string text, Toast.Type toastType, float viewDuration=1.2f) {
        var toast = ToastContainer.GetObj();
        //toast.SetPresenter(toastPresenter);
        toast.ShowToast(
            text, 
            toastType, 
            Rect.anchoredPosition, 
            toastDestination.anchoredPosition, 
            viewDuration);
    }
}
