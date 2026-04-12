using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class MarkerUIBase : UIBase, IPool
{
    public bool IsActive => IsOpened;
    protected RectTransform Rect
    {
        get
        {
            if (_rect == null) {
                _rect = GetComponent<RectTransform>();
            }

            return _rect;
        }
    }

    [SerializeField] protected Image markerImage;
    [SerializeField] protected Button actionButton;
    private RectTransform _rect;
    private int markerIndex = -1;

    public virtual void InitMarker(Sprite markerImage, UnityAction markerAction=null) {
        SetImage(markerImage);
        SetAction(markerAction);
    }

    public void SetPosition((int markerIndex, Vector2 point)? v) {
        if (v != null) {
            markerIndex = v.Value.markerIndex;
            Rect.anchoredPosition = v.Value.point;
            return;
        }

        Debug.LogError($"<color=red>marker point is null. couldn't get available marker point</color>");
    }

    private void SetImage(Sprite markerImage) {
        this.markerImage.sprite = markerImage;
    }
    private void SetAction(UnityAction markerAction) {
        var click = actionButton.onClick;
        click.RemoveAllListeners();
        click.AddListener(markerAction);
    }

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
