using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class LocationItemUI : ItemUIBase
{
    [SerializeField] ItemInfoButton infoButton;

    public override void SetUI(ItemStack item) {
        itemImage.sprite = Managers.SD.TryGetSD(item.ItemData.ItemID, out ItemSD targetSD) ? targetSD.Image : null;
        infoButton.SetItemData(item.ItemData);
    }
}
