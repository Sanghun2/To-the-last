using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionContentUI : UIBase, IPool
{
    [SerializeField] ContentUI structureContentUI;
    [SerializeField] RequirementUIContainer requirementUIContainer;
    [SerializeField] CustomButton constructionButton;
    [SerializeField] ProgressBarUI progressBarUI;

    public bool IsActive => IsOpened;

    public void Activate() {
        OpenUI();
    }

    public void Init() {
        InitUI();
        progressBarUI.Clear();
    }

    public void ShowUI(StructureSD structureSD) {
        structureContentUI.SetContentImage(structureSD.Image);
        requirementUIContainer.ShowList(structureSD.RequirementItems);
        constructionButton.InitButton(new ActionData(
            $"건설\n({structureSD.ConstructionTime}분)",
            () => {
                var a = InventoryUtility.HasIngredients(structureSD.RequirementItems);
                if (true) {
                    Managers.Construction.SetTargetStructure(structureSD);
                    Managers.Construction.ConstructSetTarget(
                        onStart: () => Managers.ScreenBlocker.SetActive(true),
                        onProgress: progressBarUI.UpdateUI,
                        onComplete: () => Managers.ScreenBlocker.SetActive(false));
                }
                else {
                    Debug.LogAssertion($"재료 불충분");
                }
            }));
        OpenUI();
    }

    public void Return() {
        CloseUI();
        //requirementUIContainer.ReleaseContainer();
    }
}
