using System;
using BilliotGames;
using UnityEngine;

public class CraftUI : UIBase
{
    [SerializeField] TitleText popUpTitleText;
    [SerializeField] ImageUI itemImageUI;
    [SerializeField] DescriptionText itemDescriptionText;
    [SerializeField] ProgressBarUI progressBarUI;
    
    public void InitItemData(ItemSD itemSD) {
        itemImageUI.SetImage(itemSD.ItemImage);
    }

    public void UpdateProgressUI(float current, float total) {
        progressBarUI.UpdateUI(current, total);
    }

    private void Reset() {
        if (popUpTitleText == null) {
            popUpTitleText = GetComponentInChildren<TitleText>();
        }

        if (itemDescriptionText == null) {
            itemDescriptionText = GetComponentInChildren<DescriptionText>();
        }

        if (itemImageUI == null) {
            itemImageUI = GetComponentInChildren<ImageUI>();
        }

        if (progressBarUI == null) {
            progressBarUI = GetComponentInChildren<ProgressBarUI>();
        }
    }
}
