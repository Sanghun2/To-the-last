using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CharacterSelectionProcess : ProcessBase<CharacterSelectProcessContext>
{
    public CharacterSelectionProcess(ProcessContextBuilder<CharacterSelectProcessContext> contextBuilder) : base(contextBuilder) {

    }

    public override bool CanComplete() {
        return true;
    }

    protected override void OnCleared() {
        Managers.UI.CloseUI<CharacterSelectionUI>();
    }

    protected override void OnComplete() {
        Managers.Player.PlayerData.SetCharacter(Managers.Character.CurrentSelectedCharacterID);
        Managers.UI.CloseUI<CharacterSelectionUI>();
    }

    protected override void OnExecute(CharacterSelectProcessContext context) {
        Managers.UI.OpenUI<GameBootStrapUI>();
        Managers.UI.OpenUI<CharacterSelectionUI>();
    }
}

public class CharacterSelectProcessContext : ProcessContext
{
    public string SelectedCharacterID => selectedCharacterID;

    private string selectedCharacterID;

    public CharacterSelectProcessContext SetSelectedCharacterID(string currentSelectedCharacterID) {
        this.selectedCharacterID = currentSelectedCharacterID;
        return this;
    }
}
