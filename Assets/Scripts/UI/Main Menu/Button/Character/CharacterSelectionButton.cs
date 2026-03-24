using BilliotGames;
using UnityEngine;

public class CharacterSelectionButton : ButtonBase
{
    private string characterID;
    private bool locked;

    public void InitCharacter(string characterID, bool locked) {
        this.characterID = characterID;
        this.locked = locked;
    }

    protected override void ButtonAction() {
        if (locked) { Debug.LogAssertion($"<color=yellow>({characterID}) is locked</color>"); return; }
        if (Managers.Character.CurrentSelectedCharacterID.Equals(characterID)) return;
        Managers.Character.CurrentSelectedCharacterID = characterID;
    }
}
