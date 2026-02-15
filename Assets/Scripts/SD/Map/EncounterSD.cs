using BilliotGames;
using UnityEngine;

public interface IEncounterExecutor
{
    void Execute(EncounterContext context);
}

public class EncounterContext
{

}

public abstract class EncounterSD : SDBase
{
    public abstract IEncounterExecutor CreateExecutor();

    private void OnValidate() {
        RenameAsset(ID, suffix:"_EncounterSD");
    }
}
