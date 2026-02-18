using BilliotGames;
using UnityEngine;

public abstract class EncounterSD : SDBase
{
    private void OnValidate() {
        RenameAsset(ID, suffix:"_EncounterSD");
    }
}
