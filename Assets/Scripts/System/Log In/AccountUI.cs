using System;
using BilliotGames;
using UnityEngine;

public class AccountUI : UIBase
{
    private AccountManager AccountManager
    {
        get
        {
            if (_accountManager == null) {
                _accountManager = FindAnyObjectByType<AccountManager>();
            }

            return _accountManager;
        }
    }

    [SerializeField] LoadigUIBase loadingUI;
    [SerializeField] AccountButtonContainer buttonContainer;
    private AccountManager _accountManager;


    public override void InitUI() {
        if (IsInit) return;

        var list = AccountManager.Methods;
        buttonContainer.Clear();
        for (int i = 0; i < list.Count; i++) {
            var method = list[i];
            var button = buttonContainer.GetObj();
            button.Init(method);
        }

        _isInit = true;
    }

    private void OnEnable() {
        Debug.Log($"[Test] ({GetType()}) event reigstered");
        _accountManager.OnProcessChanged += HandleProcessChanged;
        _accountManager.OnStateChanged += HandleStateChanged;
        //_accountManager.OnSignedIn += HandleSignedIn;
        //_accountManager.OnSignedOut += HandleSignedOut;
    }
    private void OnDisable() {
        Debug.Log($"[Test] ({GetType()}) event unregistered");
        _accountManager.OnProcessChanged -= HandleProcessChanged;
        //_accountManager.OnSignedIn -= HandleSignedIn;
        //_accountManager.OnSignedOut -= HandleSignedOut;
    }


    private void HandleProcessChanged(AccountManager.Process state) {
        switch (state) {
            case AccountManager.Process.None:
                break;
            case AccountManager.Process.SignUp:
                break;
            case AccountManager.Process.SignIn:
                break;
            case AccountManager.Process.Done:
                break;
            default:
                break;
        }
    }
    private void HandleStateChanged(AccountManager.State state) {
        switch (state) {
            case AccountManager.State.Wait:
                loadingUI?.StopLoading();
                break;
            case AccountManager.State.Processing:
                loadingUI?.StartLoading();
                break;
            default:
                break;
        }
    }
}

