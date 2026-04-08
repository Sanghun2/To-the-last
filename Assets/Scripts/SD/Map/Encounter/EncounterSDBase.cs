using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public abstract class EncounterSDBase : SDBase
{
    public Sprite EventImage => eventImage;
    public IReadOnlyList<SelectionSD> SelectionList => selectionList;
    public string FirstCategory => categoryList.Count > 0 ? categoryList[0].ID : string.Empty;


    [SerializeField] Sprite eventImage;
    [SerializeField] List<SelectionSD> selectionList = new List<SelectionSD>();

    protected override void OnValidate() {
        RenameAsset(ID, suffix:$"_{GetType()}");
    }
}
