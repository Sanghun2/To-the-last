using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CharacterSelectProcess : Process<CharacterSelectProcessContext>
{
    public CharacterSelectProcess(ProcessContextBuilder<CharacterSelectProcessContext> contextBuilder) : base(contextBuilder) {

    }

    protected override void OnCleared() {
        Managers.UI.CloseUI<CharacterSelectionUI>();
    }

    protected override void OnComplete() {
        Managers.UI.CloseUI<CharacterSelectionUI>();
    }

    protected override void OnExecuteAsync(CharacterSelectProcessContext context) {
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
