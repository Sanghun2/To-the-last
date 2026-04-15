using System;
using BilliotGames;
using UnityEngine;

public class TraitDecisionButton : ButtonBase
{
    public override void InitUI() {
        if (IsInit) return;

        base.InitUI();

        _isInit = true;
    }

    protected override void ButtonAction() {
        Managers.Process.TryCompleteCurrentProcess();
    }
    protected virtual void OnEnable() {
        Managers.Trait.OnTraitPointChanged -= UpdateButtonState;
        Managers.Trait.OnTraitPointChanged += UpdateButtonState;
        UpdateButtonState(Managers.Player.PlayerData.GetAvailableTraitPoint());
    }

    private void UpdateButtonState(int point) {
        targetButton.interactable = point >= 0;
    }
}
