using System;
using BilliotGames;
using UnityEngine;

public class CharacterDecisionButton : ButtonBase
{
    protected override void ButtonAction() {
        if (!CanDecision()) return;

        Managers.Player.PlayerData.SetCharacter(Managers.Character.CurrentSelectedCharacterID);
        Managers.Process.CompleteCurrentProcess();
        Managers.UI.CloseUI<CharacterSelectionUI>();
    }

    private bool CanDecision() {
        return true;
    }
}
