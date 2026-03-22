using BilliotGames;
using UnityEngine;

public class CharacterSelectionButton : ButtonBase
{
    private string characterID;
    private bool isUnlocked;

    public void InitCharacter(string characterID, bool isUnlocked) {
        this.characterID = characterID;
        this.isUnlocked = isUnlocked;
    }

    protected override void ButtonAction() {
        if (!isUnlocked) { Debug.LogAssertion($"<color=yellow>({characterID}) is locked</color>"); return; }
        Managers.Character.CurrentSelectedCharacterID = characterID;
    }
}
