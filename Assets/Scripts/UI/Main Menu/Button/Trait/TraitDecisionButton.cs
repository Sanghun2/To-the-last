using System;
using BilliotGames;
using UnityEngine;

public class TraitDecisionButton : ButtonBase
{
    public override void InitUI() {
        if (IsInit) return;

        base.InitUI();
        Managers.Trait.OnTraitPointChanged -= UpdateButtonState;
        Managers.Trait.OnTraitPointChanged += UpdateButtonState;
        UpdateButtonState(Managers.Player.PlayerData.GetAvailableTraitPoint());

        _isInit = true;
    }

    protected override void ButtonAction() {
        if (!CanDecision()) { Debug.Log($"특성 결정 불가"); return; }

        var selectedTrits = Managers.Trait.GetSelectedTraits();
        Managers.Player.PlayerData.SetTraits(selectedTrits);
        Managers.Process.CompleteCurrentProcess();
        Managers.UI.CloseUI<TraitSelectionUI>();
    }

    private bool CanDecision() {
        return Managers.Trait.RemainTraitPoint >= 0;
    }

    private void UpdateButtonState(int point) {
        targetButton.interactable = point >= 0;
    }
}
