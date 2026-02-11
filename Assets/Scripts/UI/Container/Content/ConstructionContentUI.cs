using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionContentUI : UIBase, IContent
{
    [SerializeField] ImageUI itemImage;
    [SerializeField] RequirementUIContainer requirementUIContainer;
    [SerializeField] TextMeshProUGUI constructionButtonText;
    [SerializeField] Button constructionButton;

    public bool IsActive => IsOpened;

    public void Activate() {
        OpenUI();
    }

    public void Init() {
        InitUI();
    }

    public void ShowUI(StructureSD structureSD) {
        itemImage.SetImage(structureSD.IconImage);
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
        constructionButton.onClick.RemoveAllListeners();
        constructionButton.onClick.AddListener(() => buttonAction?.Invoke());
    }

    public void Release() {
        CloseUI();
        //requirementUIContainer.ReleaseContainer();
    }
}
