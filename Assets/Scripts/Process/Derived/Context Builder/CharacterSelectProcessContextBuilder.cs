using UnityEngine;

public class CharacterSelectProcessContextBuilder : ProcessContextBuilder<CharacterSelectProcessContext>
{
    public override CharacterSelectProcessContext BuildTypedContext() {
        var context = new CharacterSelectProcessContext();
        context.SetSelectedCharacterID(Managers.Character.CurrentSelectedCharacterID);
        return context;
    }
}
