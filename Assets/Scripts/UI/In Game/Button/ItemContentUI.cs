using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class ItemContentUI : ButtonBase, IPool
{
    [SerializeField] Image itemImage;
    [SerializeField] RecipeSD recipeSD;

    public bool IsActive => IsOpened;

    public void Init() {
        base.InitUI();
    }
    public void Return() {
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
        itemImage.sprite = recipeSD.Outputs[0].ItemSD.Image;
        SetButtonAction(buttonAction);
    }

    protected override void ButtonAction() {
    }
}
