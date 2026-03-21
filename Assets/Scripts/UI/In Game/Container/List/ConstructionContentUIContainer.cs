using System.Collections.Generic;
using UnityEngine;

public class ConstructionContentUIContainer
    : ListContainerBase<ConstructionContentUI>
{
    public void ShowList(IReadOnlyList<StructureSD> stuctureSDList) {
        if (!IsInit) InitUI();
        var maxCount = Mathf.Max(stuctureSDList.Count, ContentCount);
        int itemCount = stuctureSDList.Count;

        for (int i = 0; i < maxCount; i++) {
            if (i < itemCount) {
                StructureSD structureSD = stuctureSDList[i];
                if (structureSD.Locked) continue;

                var constructionContentUI = GetOrCreateObj(i);
                constructionContentUI.ShowUI(structureSD);
            }
            else {
                contentList[i].CloseUI();
            }
        }
    }
}
