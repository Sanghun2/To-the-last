using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogBookData
{
    public string ID { get; }
    public IReadOnlyList<DialogPageData> Pages => pages;
    public DialogPageData CurrentPage { get; internal set; }


    IReadOnlyList<DialogPageData> pages;

    public DialogBookData(DialogBookSD dialogBookSD) {
        ID = dialogBookSD.ID;
        pages = ConvertToData(dialogBookSD.Pages);
    }

    private IReadOnlyList<DialogPageData> ConvertToData(IReadOnlyList<DialogPageSD> pages) {
        var list = new List<DialogPageData>();
        for (int i = 0; i < pages.Count; i++) {
            var pageSD = pages[i];
            list.Add(ConvertToData(pageSD));
        }

        return list;
    }

    private static DialogPageData ConvertToData(DialogPageSD pageSD) {
        return new DialogPageData(
            pageSD.ID,
            pageSD.Image,
            pageSD.TalkerName,
            pageSD.Description,
            pageSD.Selections
            );
    }
}
