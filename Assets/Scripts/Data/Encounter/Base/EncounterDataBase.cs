using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterDataBase
{
    public string ID => id;
    public Sprite EventImage => eventImage;
    public string Description => description;
    public IReadOnlyList<SelectionSDContext> SelectionList { get; }
    public int Index => index;

    private string id;
    protected Sprite eventImage;
    protected string description;
    private int index = -1;


    public EncounterDataBase(string id, Sprite eventImage, string description, IReadOnlyList<SelectionSDContext> selectList) {
        this.id = id;
        this.eventImage = eventImage;
        this.description = description;
        SelectionList = selectList;
    }

    public void SetIndex(int index) {
        this.index = index;
    }
}
