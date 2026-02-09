using System;
using System.Collections.Generic;
using BilliotGames;
using TMPro;
using UnityEngine;

public class BuildContentUI : UIBase, IContent
{
    [SerializeField] ImageUI itemImage;
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
        itemImage.SetImage(structureSD.IconImage);
        requirementUIContainer.ShowList(structureSD.RequirementItems);
        constructionButtonText.text = $"건설\n({structureSD.ConstructionTime}분)";
        OpenUI();
    }

    public void Release() {
        CloseUI();
        //requirementUIContainer.ReleaseContainer();
    }
}
