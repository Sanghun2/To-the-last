using System;
using BilliotGames;
using UnityEngine;

public class StatusCanvas : CanvasBase
{
    [SerializeField] ScreenBlocker screenBlocker;

    public override void InitUI() {
        if (IsInit) return;

        _isInit = true;

        SetActiveBlocker(Managers.UI.OpenedUICount > 0);
    }

    public void SetActiveBlocker(bool active) {
        screenBlocker.SetActive(active);
    }

    private void OnEnable() {
        Managers.UI.OnUIOpened -= UpdateBlocker;
        Managers.UI.OnUIOpened += UpdateBlocker;

        Managers.UI.OnUIClosed -= UpdateBlocker;
        Managers.UI.OnUIClosed += UpdateBlocker;
    }

    private void UpdateBlocker(UIBase _) {
        SetActiveBlocker(Managers.UI.OpenedUICount > 0);
    }


    private void OnDisable() {
        Managers.UI.OnUIOpened -= UpdateBlocker;
        Managers.UI.OnUIClosed -= UpdateBlocker;
    }
}
