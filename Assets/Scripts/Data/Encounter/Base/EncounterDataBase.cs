using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterDataBase
{
    public Sprite EventImage => eventImage;
    public string Description => description;
    public IReadOnlyList<SelectionSD> SelectionList => selectList;

    protected Sprite eventImage;
    protected string description;
    protected IReadOnlyList<SelectionSD> selectList;

    public EncounterDataBase(Sprite eventImage, string description, IReadOnlyList<SelectionSD> selectList) {
        this.eventImage = eventImage;
        this.description = description;
        this.selectList = selectList;
    }
}
