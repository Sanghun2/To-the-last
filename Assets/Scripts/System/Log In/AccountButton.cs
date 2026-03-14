using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class AccountData
{
    public Sprite Icon => icon;
    public IAccountMethod AccountMethod => accountMethod;
    public bool IsSignedUp => isSignedUp;

    [SerializeField] bool isSignedUp;
    [SerializeField] Sprite icon;
    [SerializeField] IAccountMethod accountMethod;

    public AccountData(Sprite icon, IAccountMethod method, bool isSingedUp) {
        this.icon = icon;
        this.accountMethod = method;
        this.isSignedUp = isSingedUp;
    }
}

public class AccountButton : ButtonBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] Image iconImage;
    private IAccountMethod accountMethod;

    public event Action<AccountResult> OnActionCompleted;


    public void InitButton(IAccountMethod data) {
        Init();
        iconImage.sprite = data.Icon;
        accountMethod = data;
    }

    public void Init() {
        if (IsInit) return;

        _isInit = true;
    }
    public void Return() {
        CloseUI();
    }
    public void Activate() {
        OpenUI();
    }

    protected override async void ButtonAction() {
        AccountResult accountResult = null;
        if (accountMethod.IsSignedUp) {
            Debug.Log($"try sign in");
            accountResult = await accountMethod.SignInAsync();
        }
        else {
            Debug.Log($"try sign up");
            accountResult = await accountMethod.SignUpAsync();
        }

        OnActionCompleted?.Invoke(accountResult);
    }
}
