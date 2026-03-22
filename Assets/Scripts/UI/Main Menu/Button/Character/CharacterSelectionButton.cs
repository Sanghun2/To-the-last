using BilliotGames;
using UnityEngine;

public class CharacterSelectionButton : ButtonBase
{
    private string characterID;

    public void InitCharacter(string characterID) {
        this.characterID = characterID;
    }

    protected override void ButtonAction() {
        var ui = Managers.UI.GetUI<CharacterSelectionUI>();
        ui.ShowCharacter(characterID);
    }
}
