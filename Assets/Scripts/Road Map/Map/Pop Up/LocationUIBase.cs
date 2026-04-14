using System;
using BilliotGames;
using UnityEngine;

public abstract class LocationUIBase : UIBase
{
    protected EnteranceUI EnteranceUI
    {
        get
        {
            if (_enteranceUI == null) {
                _enteranceUI = GetComponentInChildren<EnteranceUI>();
                //_enteranceUI = Managers.UI.GetUI<EnteranceUI>();
            }

            return _enteranceUI;
        }
    }

    [SerializeField] BackButton backButton;
    private EnteranceUI _enteranceUI;

    public override void InitUI() {
        if (IsInit) return;

        backButton.InitUI();
        backButton.SetButtonAction(ShowEnterance);

        _isInit = true;
    }

    public abstract void InitLocationUI(LocationBase destination);

    public virtual void ShowEnterance() {
        EnteranceUI.OpenUI();
        OnShowEnterance();
    }

    protected virtual void OnShowEnterance() { }

    public void HideEnterance() {
        EnteranceUI.CloseUI();
    }
}

public abstract class LocationUIBase<TLocation> : LocationUIBase
    where TLocation : LocationBase
{
    public override void InitLocationUI(LocationBase destination) {
        if (destination is TLocation location) {
            InitLocationUI(location);
        }
    }

    public abstract void InitLocationUI(TLocation destination);
}