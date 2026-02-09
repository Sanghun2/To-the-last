using System.Collections.Generic;
using UnityEngine;

public class BuildContentUIContainer : ListContainerBase<ConstructionContentUI>
{
    public override ConstructionContentUI GetObj() {
        for (int i = 0; i < contentList.Count; i++) {
            var content = contentList[i];
            if (!content.IsOpened) {
                return content;
            }
        }

        return CreateObj(Prefab, ContainerTr);
    }

    public override bool TryGetObj(int index, out ConstructionContentUI requirementUI) {
        if (0 <= index && index < contentList.Count) {
            requirementUI = contentList[index];
            return true;
        }

        requirementUI = null;
        return false;
    }

    public void ShowList(IReadOnlyList<StructureSD> stuctureSDList) {
        var maxCount = Mathf.Max(stuctureSDList.Count, ContentCount);
        int itemCount = stuctureSDList.Count;

        for (int i = 0; i < maxCount; i++) {
            if (i < itemCount) {
                var structureSD = stuctureSDList[i];
                if (TryGetObj(i, out ConstructionContentUI buildContentUI)) {
                    buildContentUI.ShowUI(structureSD);
                }
                else {
                    CreateObj().ShowUI(structureSD);
                }
            }
            else {
                contentList[i].CloseUI();
            }
        }
    }
}
