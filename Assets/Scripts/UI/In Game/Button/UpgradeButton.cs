using System;
using BilliotGames;
using UnityEngine;

public class UpgradeButton : ButtonBase
{
    protected override void ButtonAction() {
        Upgrade();
    }

    private void Upgrade() {
        if (!Managers.UI.TryGetOpenedUI<IUpgradeableUI>(out var ui)) { Debug.LogError($"<color=red>열려있는 upgradeable ui가 없음</color>"); return; }
        var structure = Managers.Structure.CurrentSelctedStructure;
        if (Managers.Upgrade.TryUpgrade(
            structure, 
            onProgress: ui.UpgradeUI.UpdateProgressBar,
            onComplete: ui.UpgradeUI.ClearProgressBar)) {

        }
    }
}
