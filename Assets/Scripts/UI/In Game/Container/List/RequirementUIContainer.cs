using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequirementUIContainer : ListContainerBase<RequirementUI>
{
    [SerializeField] CustomButton actioButton;

    /// <summary>
    /// 현재 보여지는 목록에 대해서 가진 아이템 수 변화 등 상태변경 시 재평가 로직 
    /// </summary>
    public void UpdateList() {

    }

    public void ShowList(IReadOnlyList<Ingredient> requirementItems) {
        if (requirementItems == null) return;
        var maxCount = Mathf.Max(requirementItems.Count, ContentCount);
        int itemCount = requirementItems.Count;

        for (int i = 0; i < maxCount; i++) {
            if (i < itemCount) {
                var requirement = requirementItems[i];
                var count = InventoryUtility.GetItemCount(requirement.ItemSD.ID);
                RequirementUI requirementUI = GetOrCreateObj(i);
                requirementUI.SetReqirementItem(requirement.ItemSD, requirement.Amount, count >= requirement.Amount);
            }
            else {
                contentList[i].CloseUI();
            }
        }
    }

    protected override void Reset() {
        base.Reset();
        if (actioButton == null) {
            actioButton = GetComponentInChildren<CustomButton>();
        }
    }
}
