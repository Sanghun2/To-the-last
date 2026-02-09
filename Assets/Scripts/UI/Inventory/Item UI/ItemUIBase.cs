using BilliotGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemUIBase : UIBase, IContent
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI amountText;

    public bool IsActive => IsOpened;

    public void Activate() {
        OpenUI();
    }
    public void Init() {
        if (IsInit) return;
        if (itemImage == null) Debug.LogError($"item image null");
        if (amountText == null) Debug.LogError($"amounts text null");
        _isInit = true;
    }
    public void Release() {
        CloseUI();
    }

    private void Reset() {
        if (itemImage == null) {
            itemImage = GetComponentInChildren<Image>();
        }

        if (amountText == null) {
            amountText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
