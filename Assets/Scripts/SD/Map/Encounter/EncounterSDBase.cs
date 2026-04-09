using System;
using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;

public abstract class EncounterSDBase : SDBase
{
    public Sprite EventImage => eventImage;
    public IReadOnlyList<SelectionSDContext> SelectionList => selectionList;
    public string FirstCategory => categoryList.Count > 0 ? categoryList[0].ID : string.Empty;


    [SerializeField] Sprite eventImage;
    [SerializeField] List<SelectionSDContext> selectionList = new List<SelectionSDContext>();

    protected override void OnValidate() {
        RenameAsset(ID, suffix:$"_{GetType()}");
    }
}

[Serializable]
public sealed class SelectionSDContext
{
    public SelectionSD SelectionSD => selectionSD;
    public SelectionRunnerSDBase SelectionRunnerSD => selectionRunnerSD;

    [SerializeField] SelectionSD selectionSD;
    [SerializeField] SelectionRunnerSDBase selectionRunnerSD;
}