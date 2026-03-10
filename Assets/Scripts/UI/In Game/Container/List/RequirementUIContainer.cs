using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequirementUIContainer : ListContainerBase<RequirementUI>
{
    [SerializeField] CustomButton actioButton;

    public void ShowList(IReadOnlyList<Ingredient> requirementItems) {
        var maxCount = Mathf.Max(requirementItems.Count, ContentCount);
        int itemCount = requirementItems.Count;

        for (int i = 0; i < maxCount; i++) {
            if (i < itemCount) {
                var ingredient = requirementItems[i];
                RequirementUI requirementUI = GetObj(i);
                requirementUI.SetReqirementItem(ingredient.ItemSD, ingredient.Amount);
            }
            else {
                contentList[i].CloseUI();
            }
        }
    }

    private void Reset() {
        if (actioButton == null) {
            actioButton = GetComponentInChildren<CustomButton>();
        }
    }
}
