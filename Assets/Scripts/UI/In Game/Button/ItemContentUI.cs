using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class ItemContentUI : ButtonBase, IPool
{
    [SerializeField] Image itemImage;
    [SerializeField] GameObject lockObj;
    private ProductionContentSD recipeSD;

    public bool IsActive => IsOpened;

    public void Init() {
        base.InitUI();
        lockObj.SetActive(false);
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
    public void SetRecipe(ProductionContentSD recipeSD, Action buttonAction) {
        this.recipeSD = recipeSD;
        itemImage.sprite = recipeSD.Outputs[0].ItemSD.Image;
        SetButtonAction(buttonAction);

        int structureLevel = Managers.Structure.CurrentSelctedStructure.StructureLevel;
        bool @lock = recipeSD.RequiredLevel > structureLevel;
        lockObj.SetActive(@lock);
    }

    protected override void ButtonAction() {
    }
}
