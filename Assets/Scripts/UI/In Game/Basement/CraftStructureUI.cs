using System;
using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class CraftStructureUI : StructureUIBase<ProductionStructureContext>
{
    [SerializeField] TextUI popUpTitleText;
    [SerializeField] DescriptionUI descriptionUI;
    [SerializeField] ItemButtonContainer itemButtonContainer;
    [SerializeField] CraftButton craftButton;
    [SerializeField] ContentUI selectedItemUI;
    [SerializeField] ProgressBarUI progressBarUI;
    [SerializeField] StructureUpgradeUI upgradeUI;
    private StructureDataParserContainer dataParserContainer = new StructureDataParserContainer();

    public override void InitUI() {
        if (IsInit) return;

        CloseUI();
        itemButtonContainer.InitUI();
        upgradeUI.InitUI();

        _isInit = true;
    }
    public override void SetUpUI(Structure structure) {
        var structureContext = structure.StructureContext as ProductionStructureContext;

        if (structureContext != null) {
            popUpTitleText.SetText(structureContext.DisplayText);
            ClearProgressUI();
            ShowList(structureContext.Data.Prouctions);
            Upgrade.InfoResult result = Managers.Upgrade.TryGetNextUpgradeInfo(structure, out StructureSD nextUpgrade);

            switch (result) {
                case Upgrade.InfoResult.InValid:
                    Debug.LogError($"<color=red>set up failed</color>");
                    return;
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

    public void InitProgressUI(float currentValue, float totalValue) {
        progressBarUI.InitUI(currentValue, totalValue);
    }
    public void UpdateProgressUI(float currentValue, float totalValue) {
        progressBarUI.UpdateUI(currentValue, totalValue);
    }
    public void ClearProgressUI() {
        progressBarUI.Clear();
    }


    private void Reset() {
        if (popUpTitleText == null) {
            popUpTitleText = GetComponentInChildren<TextUI>();
        }

        if (progressBarUI == null) {
            progressBarUI = GetComponentInChildren<ProgressBarUI>();
        }
    }
    private void OnEnable() {
        Managers.Craft.OnTargetSet -= UpdateSelectedRecipe;
        Managers.Craft.OnTargetSet += UpdateSelectedRecipe;
    }
    private void OnDisable() {
        Managers.Craft.OnTargetSet -= UpdateSelectedRecipe;
    }

    private void ShowDescription(RecipeSD recipeSD) {
        descriptionUI.InitContent(recipeSD);
    }
    private void UpdateSelectedRecipe(RecipeSD recipeSD) {
        if (recipeSD == null) { Debug.LogError($"<color=red>recipe null은 의도하지 않은 동작</color>"); return; }
        ShowDescription(recipeSD);
        craftButton.SetButtonText($"제작 ({recipeSD.RequireMinutes}분)");
        selectedItemUI.SetContentImage(recipeSD.Image);
    }
    private void ShowList(IReadOnlyList<RecipeSD> recipes) {
        InitUI();
        itemButtonContainer.ShowList(recipes);
        UpdateSelectedRecipe(recipes.First());
    }
}
