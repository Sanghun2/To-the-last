using System.Collections.Generic;
using UnityEngine;

public class ActivityContentUIContainer : ListContainerBase<ActivityContentUI>, IContentViewer
{
    protected Structure structure;

    public void SetStructure(Structure structure) {
        this.structure = structure;
    }

    public void ShowContents(IReadOnlyList<ContentSDBase> contents) {
        Clear();
        for (int i = 0; i < contents.Count; i++) {
            ContentSDBase content = contents[i];
            var contentUI = GetObjOf(i);
            contentUI.InitContent(content);
            contentUI.SetStructure(structure);
        }
    }
}
