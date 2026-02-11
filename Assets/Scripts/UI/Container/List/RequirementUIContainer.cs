using System.Collections.Generic;
using UnityEngine;

public class RequirementUIContainer : ListContainerBase<RequirementUI>
{
    public override RequirementUI GetObj() {
        for (int i = 0; i < contentList.Count; i++) {
            var content = contentList[i];
            if (!content.IsOpened) {
                return content;
            }
        }

        return CreateObj(Prefab, ContainerTr);
    }

    public override bool TryGetObj(int index, out RequirementUI requirementUI) {
        if (0 <= index && index < contentList.Count) {
            requirementUI = contentList[index];
            requirementUI.Activate();
            return true;
        }

        requirementUI = null;
        return false;
    }

    public void ShowList(IReadOnlyList<Ingredient> requirementItems) {
        var maxCount = Mathf.Max(requirementItems.Count, ContentCount);
        int itemCount = requirementItems.Count;

        for (int i = 0; i < maxCount; i++) {
            if (i < itemCount) {
                var ingredient = requirementItems[i];
                if (TryGetObj(i, out RequirementUI requirementUI)) {
                    requirementUI.SetReqirementItem(ingredient.ItemSD, ingredient.Amount);
                }
                else {
                    CreateObj().SetReqirementItem(ingredient.ItemSD, ingredient.Amount);
                }
            }
            else {
                contentList[i].CloseUI();
            }
        }
    }
}
