using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductionContentUIContainer : ListContainerBase<ProductionContentUI>, IContentViewer
{
    private Structure structure;

    public void SetStrcture(Structure structure) {
        this.structure = structure;
    }

    public virtual void ShowContents(IReadOnlyList<ContentSDBase> contents) {
        Debug.Log($"contents count? {contents.Count}");
        Clear();
        for (int i = 0; i < contents.Count; i++) {
            ContentSDBase content = contents[i];
            var contentUI = GetObjOf(i);
            contentUI.InitContent(content);
            contentUI.SetStructure(structure);
        }
    }
}
