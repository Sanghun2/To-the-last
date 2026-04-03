using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemUIBase : UIBase, IPool
{
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

    [SerializeField] protected Image itemImage;
    private RectTransform _rect;

    public bool IsActive => IsOpened;

    public abstract void SetUI(ItemEventArgs itemArgs);

    #region Pool

    public void Activate() {
        OpenUI();
    }
    public void Init() {
        if (IsInit) return;
        if (itemImage == null) Debug.LogError($"item image null");
        _isInit = true;
    }
    public void Return() {
        CloseUI();
    }

    #endregion

    protected virtual void Reset() {
        if (itemImage == null) {
            itemImage = GetComponentInChildren<Image>();
        }
    }
}
