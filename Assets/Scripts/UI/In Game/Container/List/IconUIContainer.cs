using System;
using System.Collections.Generic;
using UnityEngine;

public class IconUIContainer : ListContainerBase<IconUI>
{
    private Dictionary<string, IconUI> iconDict = new Dictionary<string, IconUI>();


    public void ActiveIcon(string iconID, bool canUpgrade) {
        if (canUpgrade) {
            ShowIcon(iconID);
        }
        else {
            HideIcon(iconID);
        }
    }
    public void ShowIcon(string iconID) {
        if (iconDict.TryGetValue(iconID, out IconUI ui)) {
            ui.OpenUI();
            return;
        }

        if (Managers.SD.TryGetSD(iconID, out IconSD targetSD)) {
            var iconUI = GetObj();
            iconUI.SetIcon(targetSD.Image);
            iconDict[iconID] = ui;
            return;
        }

        Debug.LogError($"<color=red>({iconID})에 해당하는 icon image를 찾을 수 없음</color>");
    }
    public void HideIcon(string iconID) {
        if (iconDict.TryGetValue(iconID, out var iconUI)) {
            iconUI.CloseUI();
        }
    }


    public override void Clear() {
        base.Clear();
        iconDict.Clear();
    }
}
