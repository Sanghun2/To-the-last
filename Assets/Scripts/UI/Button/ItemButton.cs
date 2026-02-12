using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : ButtonBase, IContent
{
    [SerializeField] Image itemImage;
    [SerializeField] RecipeSD recipeSD;

    public bool IsActive => IsOpened;

    public void Init() {

    }
    public void Release() {
        CloseUI();
    }
    public void Activate() {
        OpenUI();
    }


    public void ClearItem() {
        recipeSD = null;
        itemImage.sprite = null;
    }
    public void SetRecipe(RecipeSD recipeSD, Action buttonAction) {
        this.recipeSD = recipeSD;
        itemImage.sprite = recipeSD.Outputs[0].ItemSD.IconImage;
        SetButtonAction(buttonAction);
    }

    protected override void ButtonAction() {

    }
}
