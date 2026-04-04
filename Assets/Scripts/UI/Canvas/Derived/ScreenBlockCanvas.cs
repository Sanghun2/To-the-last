using System;
using UnityEngine;

public class ScreenBlockCanvas : CanvasBase
{
    public override void InitUI() {
        if (IsInit) return;

        _isInit = true;

        //CloseUI();
        //Debug.Log($"canvas init");
    }

    public void SetActive(bool active) {
        InitUI();
        gameObject.SetActive(active);
        //Debug.Log($"block active? {active}");
    }

    [ContextMenu("Active")]
    private void Active() {
        SetActive(true);
    }
}
