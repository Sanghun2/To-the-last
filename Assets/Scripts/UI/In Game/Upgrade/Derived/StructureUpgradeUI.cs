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

    public override void InitUI() {
        if (IsInit) return;

        progressBarUI.Clear();

        _isInit = true;
    }

    public override void SetUpUpgradeInfo(StructureDataBase nextStructureData) {
        SetStructureImage(nextStructureData.StructureImage);
        SetNameText(nextStructureData.DisplayText);
        ShowRequirementList(nextStructureData.RequirementItems);
    }

    public void SetUpMaxUpgrade(StructureDataBase data) {
        SetStructureImage(data.StructureImage);
        SetNameText("최고레벨");
        requirementUIContainer.Clear();
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
