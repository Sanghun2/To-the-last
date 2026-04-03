using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductionContentUIContainer : ListContainerBase<ProductionContentUI>, IContentViewer
{
    public virtual void ShowContents(IReadOnlyList<ContentSDBase> contents) {
        Debug.Log($"contents count? {contents.Count}");
        Clear();
        for (int i = 0; i < contents.Count; i++) {
            ContentSDBase content = contents[i];
            var contentUI = GetObjOf(i);
            contentUI.InitContent(content);
        }
    }
}
