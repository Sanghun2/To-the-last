using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        if (IsInit) return; 

        InitUI();
        progressBarUI.Clear();

        _isInit = true;
    }

    public void ShowUI(StructureSD structureSD) {
        structureContentUI.SetContentImage(structureSD.Image);
        requirementUIContainer.ShowList(structureSD.RequirementItems);
        progressBarUI.Clear();
        constructionButton.InitButton(new ActionData(
            $"건설\n({structureSD.ConstructionTime}분)",
            () => {
                var a = InventoryUtility.HasIngredients(structureSD.RequirementItems);
                if (true) {
                    Managers.Construction.SetTargetStructure(structureSD);
                    Managers.Construction.ConstructCurrentTarget(
                        onProgress: progressBarUI.UpdateUI,
                        onComplete: () => {
                            progressBarUI.Clear();
                            Managers.UI.CloseUI<ConstructionUI>();
                        });
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
