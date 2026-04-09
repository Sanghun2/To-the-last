using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterDataBase
{
    public Sprite EventImage => eventImage;
    public string Description => description;
    public IReadOnlyList<SelectionSDContext> SelectionList => selectList;
    public int Index => index;

    protected Sprite eventImage;
    protected string description;
    protected IReadOnlyList<SelectionSDContext> selectList;
    private int index = -1;


    public EncounterDataBase(Sprite eventImage, string description, IReadOnlyList<SelectionSDContext> selectList) {
        this.eventImage = eventImage;
        this.description = description;
        this.selectList = selectList;
    }

    public void SetIndex(int index) {
        this.index = index;
    }
}
