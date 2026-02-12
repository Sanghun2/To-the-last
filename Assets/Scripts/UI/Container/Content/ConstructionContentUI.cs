using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionContentUI : ContentUI, IContent
{
    [SerializeField] RequirementUIContainer requirementUIContainer;
    [SerializeField] TextMeshProUGUI constructionButtonText;

    public bool IsActive => IsOpened;

    public void Activate() {
        OpenUI();
    }

    public void Init() {
        InitUI();
    }

    public void ShowUI(StructureSD structureSD) {
        contentImage.SetImage(structureSD.IconImage);
        requirementUIContainer.ShowList(structureSD.RequirementItems);
        constructionButtonText.text = $"건설\n({structureSD.ConstructionTime}분)";
        SetButtonAction(() => {
            if (Managers.Construction.HasEnoughItems(structureSD.RequirementItems)) {
                Managers.Construction.SetTargetStructure(structureSD);
                Managers.Construction.ConstructTarget();
            }
            else {
                Debug.LogAssertion($"재료 불충분");
            }
        });
        OpenUI();
    }

    private void SetButtonAction(Action buttonAction) {
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() => buttonAction?.Invoke());
    }

    public void Release() {
        CloseUI();
        //requirementUIContainer.ReleaseContainer();
    }
}
