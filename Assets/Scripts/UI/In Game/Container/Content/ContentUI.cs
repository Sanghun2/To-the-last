using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
