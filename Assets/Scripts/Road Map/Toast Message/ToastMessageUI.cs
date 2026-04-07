using System;
using System.Collections.Generic;
using BilliotGames;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessageUI : UIBase, IPool
{
    public bool IsActive => IsOpened;
    public RectTransform Rect
    {
        get
        {
            if (_rect == null) {
                _rect = GetComponent<RectTransform>();
            }

            return _rect;
        }
    }
    public CanvasGroup CanvasGroup
    {
        get
        {
            if (_canvasGroup == null) {
                _canvasGroup = GetComponentInChildren<CanvasGroup>();
            }

            return _canvasGroup;
        }
    }
    public RectRebuilder RectRebuilder
    {
        get
        {
            if (_rectRebuilder == null) {
                _rectRebuilder = GetComponentInChildren<RectRebuilder>();
            }

            return _rectRebuilder;
        }
    }

    [SerializeField] TextMeshProUGUI messageText;
    [Space]
    [SerializeField] GameObject imageObj;
    [SerializeField] Image iconImage;
    [SerializeField] Image colorImage;
    [Space] 
    [SerializeField] List<Color> typeColor;
    private RectTransform _rect;
    private CanvasGroup _canvasGroup;
    private RectRebuilder _rectRebuilder;
    private ToastPresenterBase presenter
        = new DotweenToastPresenter(Ease.OutBack, Ease.OutQuart, 0.5f, 0.35f);

    #region Toast

    public void SetPresenter(ToastPresenterBase presenter) {
        this.presenter = presenter;
    }

    public void ShowToast(string text, Toast.Type toastType, Vector2 startPos, Vector2 endPos, float viewDuration) {
        InitToast(text, toastType, startPos);
        RectRebuilder.Rebuild();
        presenter.PresentToast(this, endPos, viewDuration);
        PlaySound(toastType);
    }

    private void PlaySound(Toast.Type toastType) {
        switch (toastType) {
            case Toast.Type.None:
                break;
            case Toast.Type.Info:
                break;
            case Toast.Type.Warning:
                Managers.Sound.PlaySound(Define.Sound.WARNING);
                break;
            case Toast.Type.Error:
                Managers.Sound.PlaySound(Define.Sound.ERROR);
                break;
            case Toast.Type.Confirm:
                Managers.Sound.PlaySound(Define.Sound.CONFIRM);
                break;
            default:
                break;
        }
    }

    private void InitToast(string text, Toast.Type toastType, Vector2 startPos) {
        Rect.anchoredPosition = startPos;
        messageText.text = text;
        if (Managers.SD.TryGetSD($"toast{toastType}", out IconSD icon)) {
            iconImage.sprite = icon.Image;
            colorImage.color = typeColor[(int)toastType];
        }
        imageObj.SetActive(toastType != Toast.Type.None);
    }

    #endregion

    #region Pool

    public void Activate() {
        OpenUI();
    }
    public void Init() {
        InitUI();
    }
    public void Return() {
        CloseUI();
    }


    #endregion
}
