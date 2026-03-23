using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CharacterSelectProcess : Process<CharacterSelectProcessContext>
{
    public CharacterSelectProcess(ProcessContextBuilder<CharacterSelectProcessContext> contextBuilder) : base(contextBuilder) {

    }

    public override UniTask ExecuteProcessAsync(CharacterSelectProcessContext context, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
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
