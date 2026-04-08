using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterDataBase
{
    public Sprite EventImage => eventImage;
    public string Description => description;
    public IReadOnlyList<SelectionPair> SelectionList => selectList;
    public int Index => index;

    protected Sprite eventImage;
    protected string description;
    protected IReadOnlyList<SelectionPair> selectList;
    private int index = -1;


    public EncounterDataBase(Sprite eventImage, string description, IReadOnlyList<SelectionPair> selectList) {
        this.eventImage = eventImage;
        this.description = description;
        this.selectList = selectList;
    }

    public void SetIndex(int index) {
        this.index = index;
    }
}
