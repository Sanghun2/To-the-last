using BilliotGames;
using UnityEngine;

public sealed class ScreenBlocker : UIBase
{
    public override void InitUI() {
        if (IsInit) return;

        CloseUI();

        _isInit = true;
    }

    public void SetActive(bool active) {
        InitUI();
        gameObject.SetActive(active);
    }
}
