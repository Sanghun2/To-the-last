using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Image + Button Action 기능
/// </summary>
public class ContentUI : UIBase
{
    [SerializeField] protected Image contentImage;
    [SerializeField] protected Button actionButton;

    public virtual void SetContentImage(Sprite image) {
        if (contentImage != null) {
            contentImage.sprite = image;
        }
    }

    public virtual void SetButtonAction(UnityAction buttonAction) {
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(buttonAction);
    }
}
