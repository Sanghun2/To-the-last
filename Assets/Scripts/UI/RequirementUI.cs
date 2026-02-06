using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequirementUI : ButtonBase
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI amountText;
    private string itemID;
    private int itemAmount;

    public void SetReqirementItem(ItemStack itemStack) {
        itemID = itemStack.ItemData.ItemID;
        itemAmount = itemStack.Amount;
        UpdateUI(itemID, itemAmount);
    }

    protected override void ButtonAction() {
        // show item info
    }

    protected override void Reset() {
        base.Reset();

        if (itemImage == null) {
            itemImage = GetComponentInChildren<Image>();
        }

        if (amountText == null) {
            amountText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void UpdateUI(string itemID, int amount) {
        if (Managers.SD.TryGetSD<ItemSD>(itemID, out ItemSD targetSD)) {
            itemImage.sprite = targetSD.ItemImage;
        }
        amountText.text = $"x{amountText}";
    }
}
