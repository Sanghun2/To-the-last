using System.Linq;
using UnityEngine;

public sealed class CharacterTester : MonoBehaviour
{
    [SerializeField] CharacterSD testCharacter;
    [SerializeField] CharacterSD[] sampleCharacters;

    public void ShowCharacter() {
        var ui = Managers.UI.OpenUI<CharacterSelectionUI>();
        ui.ShowCharacter(testCharacter.ID);
    }

    public void ShowCharacterList() {
        var ui = Managers.UI.OpenUI<CharacterSelectionUI>();
        ui.InitCharacterButtons(sampleCharacters.Select(c => new Character(c.ToData())).ToList());
    }
}
