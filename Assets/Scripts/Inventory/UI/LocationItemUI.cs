using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class LocationItemUI : ItemUIBase
{
    [SerializeField] ItemInfoButton infoButton;

    public override void SetUI(ItemEventArgs itemArgs) {
        itemImage.sprite = Managers.SD.TryGetSD(itemArgs.itemID, out ItemSD targetSD) ? targetSD.Image : null;
        if (Managers.SD.TryGetSD(itemArgs.itemID, out ItemSD itemSD)) {
            infoButton.SetItemData(itemSD.ToData());
        }
    }
}
