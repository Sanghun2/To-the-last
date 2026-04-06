using System.Collections.Generic;
using UnityEngine;

public class ConstructionContentUIContainer
    : ListContainerBase<ConstructionContentUI>
{
    public void ShowList(IReadOnlyList<StructureSDBase> stuctureSDList) {
        if (!IsInit) InitUI();
        var maxCount = Mathf.Max(stuctureSDList.Count, ContentCount);
        int itemCount = stuctureSDList.Count;

        for (int i = 0; i < maxCount; i++) {
            if (i < itemCount) {
                StructureSDBase structureSD = stuctureSDList[i];
                if (structureSD.Locked) continue;

                var constructionContentUI = GetOrCreateObj(i);
                constructionContentUI.InitContent(structureSD);
            }
            else {
                contentList[i].CloseUI();
            }
        }
    }
}
