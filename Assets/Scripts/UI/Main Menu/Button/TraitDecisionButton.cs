using System;
using BilliotGames;
using UnityEngine;

public class TraitDecisionButton : ButtonBase
{
    public override void InitUI() {
        if (IsInit) return;

        Managers.Trait.OnTraitPointChanged -= UpdateButtonState;
        Managers.Trait.OnTraitPointChanged += UpdateButtonState;
        UpdateButtonState(Managers.Player.PlayerData.GetAvailableTraitPoint());

        _isInit = true;
    }

    protected override void ButtonAction() {
        var ui = Managers.UI.GetUI<TraitSelectionUI>();
        var selectedTrits = ui.GetSelectedTraits();
        Managers.Player.PlayerData.SetTraits(selectedTrits);
    }

    private void UpdateButtonState(int point) {
        targetButton.interactable = point >= 0;
    }
}
