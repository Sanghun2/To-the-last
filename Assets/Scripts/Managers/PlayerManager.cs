using BilliotGames;
using UnityEngine;

public class PlayerManager : IInitializable
{
    public bool IsInit => _isInit;
    public InventoryBase Inventory => player.Inventory;
    public Player Player => player;

    private Player player = new Player();

    private bool _isInit;

    public void Init() {
        if (IsInit) return;

        player.Init();

        _isInit = true;
    }

    public void Release() {

    }
}
