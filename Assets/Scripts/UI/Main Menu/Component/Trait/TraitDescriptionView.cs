using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class TraitDescriptionView : UIBase
{
    [SerializeField] Image traitImage;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI traitNameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    public override void InitUI() {
        if (IsInit) return;

        ClearDescription();

        _isInit = true;
    }

    public void ClearDescription() {
        traitImage.gameObject.SetActive(false);
        costText.text = string.Empty;
        traitNameText.text = string.Empty;
        descriptionText.text = string.Empty;
    }
    public void ShowDescription(Trait trait) {
        traitImage.sprite = trait.Data.IconImage;
        traitImage.gameObject.SetActive(true);

        traitNameText.text = trait.Data.DisplayText;
        this.descriptionText.text = trait.Data.Descripion;
        costText.SetText("Cost: {0}", trait.Data.Cost);
    }
    public void ShowDescription(TraitUI traitUI) {
        ShowDescription(traitUI.Trait);
    }
}
