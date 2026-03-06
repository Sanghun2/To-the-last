using BilliotGames;
using UnityEngine;

public class PoolableTextUI : TextUI, IPool
{
    public bool IsActive => IsOpened;

    public void Init() {
        if (IsInit) return;
        InitUI();
        _isInit = true;
    }

    public void Activate() {
        OpenUI();
    }
    public void Return() {
        CloseUI();
    }
}
