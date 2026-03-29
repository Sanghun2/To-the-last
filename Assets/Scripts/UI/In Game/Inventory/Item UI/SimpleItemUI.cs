using System;
using BilliotGames;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class SimpleItemUI : ItemUIBase
{
    [Space]
    [SerializeField] protected TextMeshProUGUI amountText;
    [SerializeField] protected TextMeshProUGUI itemNameText;

    public override void SetUI(ItemStack item) {
        var itemData = item.ItemData;
        if (Managers.SD.TryGetSD(itemData.ItemID, out ItemSD targetSD)) {
            itemImage.sprite = targetSD.Image;
            itemNameText.text = targetSD.DisplayText;
            amountText.text = GetAmountText(item.Amount);
            Activate();
        }
        else {
            Debug.LogError($"<color=red>{itemData.ItemID}에 해당하는 SD가 없음</color>");
        }
    }

    internal void UpdateUI(ItemStack itemStack, int deltaAmount) {
        if (itemStack.Amount > 0) {
            amountText.text = GetAmountText(itemStack.Amount);
        }
        else {
            Return();
        }
    }

    private string GetAmountText(int amount) {
        return $"x {amount}";
    }
}
