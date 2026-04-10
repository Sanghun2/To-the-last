using System.Collections.Generic;
using UnityEngine;

public class DialogEncounterData : EncounterDataBase
{
    public DialogBookData BookData { get; }

    public DialogEncounterData(
        string id, 
        Sprite eventImage, 
        string description, 
        IReadOnlyList<SelectionSDContext> selectionList, 
        DialogBookSD bookData) 
        : base(id, eventImage, description, selectionList) {

        BookData = new DialogBookData(bookData);
    }
}
