using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class TraitDescriptionUI : UIBase
{
    [SerializeField] Image traitImage;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI traitNameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    public void ShowDescription(Trait trait) {
        traitImage.sprite = trait.Data.IconImage;
        traitNameText.text = trait.Data.DisplayText;
        this.descriptionText.text = trait.Data.Descripion;
        costText.SetText("Cost: {0}", trait.Data.Cost);
    }
}
