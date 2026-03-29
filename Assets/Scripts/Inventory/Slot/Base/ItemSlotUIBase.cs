using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSlotUIBase : UIBase, IPool
{
    public bool IsActive => IsOpened;

    //public abstract event Action<ItemStack> OnItemSet;
    //public abstract event Action OnItemRemoved;

    public abstract void SetSlotUI(ItemStack item);
    public abstract void ClearItem();

    #region Pool

    public void Init() {
        InitUI();
    }
    public void Activate() {
        OpenUI();
    }
    public void Return() {
        CloseUI();
    }

    #endregion
}
