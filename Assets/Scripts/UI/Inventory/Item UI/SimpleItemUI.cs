using System;
using BilliotGames;
using TMPro;
using UnityEngine;

public class SimpleItemUI : ItemUIBase
{
    [Space]
    [SerializeField] TextMeshProUGUI itemNameText;

    public void InitItem(ItemData itemData, int amount) {
        if (Managers.SD.TryGetSD(itemData.ItemID, out ItemSD targetSD)) {
            itemImage.sprite = targetSD.IconImage;
            itemNameText.text = targetSD.DisplayName;
            amountText.text = GetAmountText(amount);
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
            Release();
        }
    }

    private string GetAmountText(int amount) {
        return $"x {amount}";
    }
}
