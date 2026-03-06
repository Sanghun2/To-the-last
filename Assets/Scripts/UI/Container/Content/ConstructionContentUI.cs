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

    public bool IsActive => IsOpened;

    public void Activate() {
        OpenUI();
    }

    public void Init() {
        InitUI();
    }

    public void ShowUI(StructureSD structureSD) {
        structureContentUI.SetContentImage(structureSD.IconImage);
        requirementUIContainer.ShowList(structureSD.RequirementItems);
        constructionButton.InitButton(new ActionData(
            $"건설\n({structureSD.ConstructionTime}분)",
            () => {
                if (Managers.Construction.HasEnoughItems(structureSD.RequirementItems)) {
                    Managers.Construction.SetTargetStructure(structureSD);
                    Managers.Construction.ConstructTarget();
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
