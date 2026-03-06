using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequirementUI : UIBase, IPool
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI amountText;
    private string itemID;
    private int itemAmount;

    public bool IsActive => IsOpened;

    public void Activate() {
        OpenUI();
    }

    public void Init() {
        InitUI();
    }

    public void Return() {
        CloseUI();
    }

    public void SetReqirementItem(Ingredient requirement) {
        SetReqirementItem(requirement.ItemSD, requirement.Amount);
    }
    public void SetReqirementItem(ItemSD itemSD, int amount) {
        itemID = itemSD.ID;
        itemAmount = amount;
        UpdateUI(itemID, itemAmount);
    }

    protected virtual void Reset() {

        if (itemImage == null) {
            itemImage = GetComponentInChildren<Image>();
        }

        if (amountText == null) {
            amountText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    protected virtual void UpdateUI(string itemID, int amount) {
        if (Managers.SD.TryGetSD(itemID, out ItemSD targetSD)) {
            itemImage.sprite = targetSD.ItemImage;
        }
        amountText.text = $"x{amount}";
    }

}
