using System;
using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class CraftStructureUI : StructureUIBase<ProductionStructureContext>, IUpgradeableUI
{
    public Structure Structure => structure;
    public StructureUpgradeUI UpgradeUI => upgradeUI;
    private ProductionStructureContext Context => structure.StructureContext as ProductionStructureContext;

    // confirmed
    [SerializeField] StructureUpgradeUI upgradeUI;
    [SerializeField] ProductionContentUIContainer productionContentUIContainer;

    //// obsolete
    //[SerializeField] DescriptionUI descriptionUI;
    //[SerializeField] ItemButtonContainer itemButtonContainer;
    //[SerializeField] CraftButton craftButton;
    //[SerializeField] ContentUI selectedItemUI;
    //[SerializeField] ProgressBarUI progressBarUI;

    //// not confirmed

    private StructureDataParserContainer dataParserContainer = new StructureDataParserContainer();
    private Structure structure;

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();
        productionContentUIContainer.Clear();
        upgradeUI.InitUI();

        _isInit = true;
    }
    public override void SetUpUI(Structure structure) {
        InitUI();
        this.structure = structure;
        var structureContext = Context;

        if (structureContext != null) {
            //ClearProgressUI();
            titleText.SetText(structureContext.DisplayText);
            productionContentUIContainer.ShowContents(structureContext.Data.Prouctions);
            //ShowList(structureContext.Data.Prouctions);
        }

        structure.OnUpgraded -= UpdateUpgradeInfo;
        structure.OnUpgraded += UpdateUpgradeInfo;
        UpdateUpgradeInfo(structure);

        structure.OnUpgraded -= UpdateContentView;
        structure.OnUpgraded += UpdateContentView;
        UpdateContentView();
    }


    //public void InitProgressUI(float currentValue, float totalValue) {
    //    progressBarUI.InitUI(currentValue, totalValue);
    //}
    //public void UpdateProgressUI(float currentValue, float totalValue) {
    //    progressBarUI.UpdateUI(currentValue, totalValue);
    //}
    //public void ClearProgressUI() {
    //    progressBarUI.Clear();
    //}

    private void OnEnable() {
        //Managers.Craft.OnTargetSet -= UpdateSelectedRecipe;
        //Managers.Craft.OnTargetSet += UpdateSelectedRecipe;
    }
    private void OnDisable() {
        //Managers.Craft.OnTargetSet -= UpdateSelectedRecipe;

        if (structure != null) {
            structure.OnUpgraded -= UpdateUpgradeInfo;
            structure.OnUpgraded -= UpdateContentView;
            structure = null;
        }
    }

    //private void ShowDescription(ProductionContentSD recipeSD) {
    //    descriptionUI.InitContent(recipeSD);
    //}
    //private void UpdateSelectedRecipe(ProductionContentSD recipeSD) {
    //    if (recipeSD == null) { Debug.LogError($"<color=red>recipe null은 의도하지 않은 동작</color>"); return; }
    //    ShowDescription(recipeSD);
    //    craftButton.SetButtonText($"제작 ({recipeSD.RequireMinutes}분)");
    //    selectedItemUI.SetContentImage(recipeSD.Image);
    //}

    //[Obsolete("content list를 보여주는 예전 방식")]
    //private void ShowList(IReadOnlyList<ProductionContentSD> recipes) {
    //    InitUI();
    //    itemButtonContainer.ShowList(recipes);
    //    UpdateSelectedRecipe(recipes.First());
    //}

    private void UpdateContentView() => UpdateContentView(null);
    private void UpdateContentView(Structure _) {
        productionContentUIContainer.ShowContents(Context.Data.Prouctions);
    }

    private void UpdateUpgradeInfo(Structure structure) {
        SetTitleText($"{structure.DisplayText}");
        Upgrade.InfoResult result = Managers.Upgrade.TryGetNextUpgradeInfo(structure, out StructureSD nextUpgrade);

        switch (result) {
            case Upgrade.InfoResult.InValid:
            case Upgrade.InfoResult.MaxLevel:
                upgradeUI.SetUpMaxUpgrade(structure.StructureContext.Data);
                break;
            case Upgrade.InfoResult.Available:
                if (!dataParserContainer.TryGet(nextUpgrade, out var parser)) return;
                upgradeUI.SetUpUpgradeInfo(parser.ParseData(nextUpgrade));
                break;
            default:
                break;
        }
    }
}
