using BilliotGames;
using UnityEngine;

public class PlayerManager : IInitializable
{
    public bool IsInit => _isInit;
    public InventoryBase Inventory => _inventory;

    // inven
    private InventoryBase _inventory = new SimpleInventory("player inventory", 100);

    // stat

    private bool _isInit;

    public void Init() {
        if (IsInit) return;



        _isInit = true;
    }

    public void Release() {

    }
}
