using BilliotGames;
using UnityEngine;

public class MarkerUIBase : UIBase, IPool
{
    public bool IsActive => IsOpened;



    #region Pool

    public void Activate() {
        OpenUI();
    }
    public void Init() {
        InitUI();
    }
    public void Return() {
        CloseUI();
    }

    #endregion
}
