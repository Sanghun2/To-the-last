using System;
using DG.Tweening;
using UnityEngine;

public class ToastTester : MonoBehaviour
{

    [SerializeField] string testText;
    [SerializeField] Toast.Type toastType;
    [SerializeField] Ease slideIn;
    [SerializeField] Ease fadeOut;
    [SerializeField] float viewDuration = 0.75f;
    [SerializeField] float slideInDuration = 0.75f;
    [SerializeField] float fadeOutDuration = 0.4f;

    public void SetPresenter() {
        Managers.Toast.SetPresenter(new DotweenToastPresenter(slideIn, fadeOut, slideInDuration, fadeOutDuration));
    }

    public void ShowToast() {
        Managers.Toast.ShowToast(testText, toastType, viewDuration);
    }
}
