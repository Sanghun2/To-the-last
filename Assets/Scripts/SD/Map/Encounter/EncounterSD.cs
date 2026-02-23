using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public abstract class EncounterSD : SDBase
{
    public Sprite EventImage => eventImage;
    public IReadOnlyList<SelectionSD> SelectionList => selectionList;

    [SerializeField] Sprite eventImage;
    [SerializeField] List<SelectionSD> selectionList = new List<SelectionSD>();

    private void OnValidate() {
        RenameAsset(ID, suffix:"_EncounterSD");
    }
}
