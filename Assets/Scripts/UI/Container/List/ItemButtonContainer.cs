using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemButtonContainer : ListContainerBase<ItemContentUI>
{
    public void ShowList(IReadOnlyList<RecipeSD> recipes) {
        var count = Mathf.Max(recipes.Count, ContentCount);
        for (int i = 0; i < count; i++) {
            if (i < recipes.Count) {
                var recipe = recipes[i];
                var contentUI = GetObj(i);
                contentUI.SetRecipe(recipe, () => Managers.UI.GetUI<CraftStructureUI>().SetRecipe(recipe));
            }
            else {
                contentList[i].CloseUI();
            }
        }
    }
}
