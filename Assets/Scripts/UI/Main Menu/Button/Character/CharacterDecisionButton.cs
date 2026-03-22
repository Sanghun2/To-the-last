using BilliotGames;
using UnityEngine;

public class CharacterDecisionButton : ButtonBase
{
    protected override void ButtonAction() {
        Managers.Player.PlayerData.SetCharacter(Managers.Character.CurrentSelectedCharacterID);
    }
}
