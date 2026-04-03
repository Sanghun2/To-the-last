using System.Collections.Generic;
using UnityEngine;

public class UtilityContentUIContainer : ListContainerBase<ActivityContentUI>, IContentViewer
{
    public void ShowContents(IReadOnlyList<ContentSDBase> contents) {
        Clear();
        for (int i = 0; i < contents.Count; i++) {
            ContentSDBase content = contents[i];
            var contentUI = GetObjOf(i);
            contentUI.InitContent(content);
        }
    }
}
