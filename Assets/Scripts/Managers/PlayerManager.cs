using BilliotGames;
using UnityEngine;

public class PlayerManager : IInitializable
{
    public bool IsInit => _isInit;
    public InventoryBase Inventory => playerData.Inventory;
    public PlayerData PlayerData => playerData;

    private PlayerData playerData = new PlayerData();

    private bool _isInit;

    public void Init() {
        if (IsInit) return;

        playerData.Init();

        _isInit = true;
    }

    public void Release() {

    }
}
