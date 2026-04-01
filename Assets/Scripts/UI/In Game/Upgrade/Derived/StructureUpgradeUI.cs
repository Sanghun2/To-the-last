using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureUpgradeUI : UpgradeUIBase<StructureDataBase>
{
    [SerializeField] Image structureImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] ProgressBarUI progressBarUI;
    [SerializeField] CustomButton requirementInfoButton;
    [SerializeField] RequirementUIContainer requirementUIContainer;
    [SerializeField] UpgradeButton upgradeButton;

    public override void InitUI() {
        if (IsInit) return;

        progressBarUI.Clear();
        requirementUIContainer.Clear();

        _isInit = true;
    }

    public override void SetUpUpgradeInfo(StructureDataBase nextStructureData) {
        InitUI();
        SetStructureImage(nextStructureData.StructureImage);
        SetNameText(nextStructureData.DisplayText);
        ShowRequirementList(nextStructureData.RequirementItems);
        SetActiveUpgradeUI(true);
    }

    public void SetUpMaxUpgrade(StructureDataBase data) {
        InitUI();
        SetStructureImage(data.StructureImage);
        nameText.text = "최고레벨";
        requirementUIContainer.Clear();
        SetActiveUpgradeUI(false);
    }

    private void SetActiveUpgradeUI(bool active) {
        upgradeButton.gameObject.SetActive(active);
        progressBarUI.gameObject.SetActive(active);
    }

    public void UpdateProgressBar(float currentValue, float maxValue) {
        progressBarUI.UpdateUI(currentValue, maxValue);
    }
    public void ClearProgressBar() {
        progressBarUI.Clear();
    }

    private void SetStructureImage(Sprite image) {
        structureImage.sprite = image;
    }
    private void SetNameText(string upgradeTargetName) {
        nameText.text = $"{upgradeTargetName} 업그레이드";
    }
    private void ShowRequirementList(IReadOnlyList<Ingredient> ingredients) {
        requirementUIContainer.ShowList(ingredients);
    }

}
