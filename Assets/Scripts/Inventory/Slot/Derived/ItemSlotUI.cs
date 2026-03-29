using System;
using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotUI : ItemSlotUIBase, IDropHandler
{
    [SerializeField] protected Transform itemContainer;
    [SerializeField] TextMeshProUGUI amountText;
    private ItemUIBase itemUI;

    //public override event Action<ItemStack> OnItemSet;
    //public override event Action OnItemRemoved;

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("item dropped");
    }

    public override void SetSlotUI(ItemStack item) {
        if (item == null) return;

        if (itemUI == null) {
            var container = Managers.UI.GetUI<LocationItemUIContainer>();
            itemUI = container.GetObj();
            itemUI.transform.SetParent(itemContainer);
            itemUI.Rect.anchoredPosition = Vector2.zero;
        }

        itemUI.SetUI(item);
        item.OnItemRemoved -= ClearItem;
        item.OnItemRemoved += ClearItem;
        amountText.SetText("{0}", item.Amount);
        //OnItemSet?.Invoke(item);
    }

    public override void ClearItem() {
        itemUI.transform.SetParent(Managers.UI.GetUI<LocationItemUIContainer>().ContainerTr);
        itemUI?.Return();
        itemUI = null;
        Return();
    }
}
