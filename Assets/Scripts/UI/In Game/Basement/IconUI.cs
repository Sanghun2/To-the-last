using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class IconUI : UIBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] Image iconImage;

    public void SetIcon(Sprite image) {
        iconImage.sprite = image;
    }

    #region Pool

    public void Activate() {
        OpenUI();
    }
    public void Init() {
        if (IsInit) return;

        InitUI();

        _isInit = true;
    }
    public void Return() {
        CloseUI();
    }

    #endregion
}
