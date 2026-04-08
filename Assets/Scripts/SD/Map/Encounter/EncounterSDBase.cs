using System;
using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;

public abstract class EncounterSDBase : SDBase
{
    public Sprite EventImage => eventImage;
    public IReadOnlyList<SelectionSDBase> SelectionList => selectionList.Select(x => x.SelectionSD).ToList();
    public string FirstCategory => categoryList.Count > 0 ? categoryList[0].ID : string.Empty;


    [SerializeField] Sprite eventImage;
    [SerializeField] List<SelectionPair> selectionList = new List<SelectionPair>();

    protected override void OnValidate() {
        RenameAsset(ID, suffix:$"_{GetType()}");
    }
}

[Serializable]
public sealed class SelectionPair
{
    public SelectionSDBase SelectionSD => selectionSD;
    public SelectionRunnerSDBase SelectionRunnerSD => selectionRunnerSD;

    [SerializeField] SelectionSDBase selectionSD;
    [SerializeField] SelectionRunnerSDBase selectionRunnerSD;
}