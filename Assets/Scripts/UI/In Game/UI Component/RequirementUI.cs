using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequirementUI : UIBase, IPool
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] ItemInfoButton itemInfoButton;
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

    public void SetReqirementItem(ItemSD itemSD, int amount, bool enough=true) {
        itemID = itemSD.ID;
        itemAmount = amount;
        UpdateUI(itemID, itemAmount, enough);

        itemInfoButton.SetData(itemID);
    }
    public void SetReqirementItem(Ingredient requirement, bool enough=true) {
        SetReqirementItem(requirement.ItemSD, requirement.Amount, enough);
    }

    protected virtual void Reset() {

        if (itemImage == null) {
            itemImage = GetComponentInChildren<Image>();
        }

        if (amountText == null) {
            amountText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    protected virtual void UpdateUI(string itemID, int amount, bool enough=true) {
        if (Managers.SD.TryGetSD(itemID, out ItemSD targetSD)) {
            itemImage.sprite = targetSD.Image;
        }
        amountText.SetText("x{0}", amount);
        amountText.color = enough ? Color.white : Color.red;
    }
}
