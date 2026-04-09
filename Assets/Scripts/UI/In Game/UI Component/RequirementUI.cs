using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class RequirementUI : UIBase, IPool
{
    [SerializeField] Image requirementImage;
    [SerializeField] TextMeshProUGUI requirementText;
    [SerializeField] ItemInfoButton itemInfoButton;

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

    public void SetReqirementUI(Sprite requirementImage, string requirementText, bool isMet, Action infoAction=null) {
        UpdateUI(requirementImage, requirementText, isMet);

        //itemInfoButton.SetData(itemID);
    }
    public void SetReqirementUI(string itemID, Sprite image, int amount, bool isMet) {
        SetImage(image);
        requirementText.SetText("x {0}", amount);
        requirementText.gameObject.SetActive(amount >= 0);
        SetTextColor(isMet);

        itemInfoButton.SetData(itemID);
    }

    protected virtual void Reset() {

        if (requirementImage == null) {
            requirementImage = GetComponentInChildren<Image>();
        }

        if (requirementText == null) {
            requirementText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    protected virtual void UpdateUI(Sprite image, string text, bool isMet) {
        SetImage(image);
        SetText(text);
        SetTextColor(isMet);
    }


    private void SetImage(Sprite image) {
        requirementImage.sprite = image;
        requirementImage.gameObject.SetActive(image != null);
    }
    private void SetTextColor(bool isMet) {
        requirementText.color = isMet ? Color.white : Color.red;
    }
    private void SetText(string text) {
        requirementText.gameObject.SetActive(string.IsNullOrEmpty(text) == false);
        requirementText.text = text;
    }

}
