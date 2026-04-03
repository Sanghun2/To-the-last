using System;
using UnityEngine;

public class ScreenBlockCanvas : CanvasBase
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
