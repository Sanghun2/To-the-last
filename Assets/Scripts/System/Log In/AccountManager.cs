using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AccountManager : MonoBehaviour, IInitializable
{
    public enum Process
    {
        None,
        SignUp,
        SignIn,
        Done,
    }

    public enum State {
        Wait,
        Processing,
    }

    public bool IsInit => _isInit;
    public IReadOnlyList<IAccountMethod> Methods => methods;

    private Process CurrentProcess
    {
        get => currentProcess;
        set
        {
            var prevState = currentProcess;
            currentProcess = value;
            if (currentProcess != prevState) {
                OnProcessChanged?.Invoke(currentProcess);
            }
        }
    }
    private State CurrentState
    {
        get => currentState;
        set
        {
            var prevState = currentState;
            currentState = value;
            if (currentState != prevState) {
                OnStateChanged?.Invoke(currentState);
            }
        }
    }

    private Process currentProcess;
    private State currentState;
    private bool _isInit;

    private List<IAccountMethod> methods;

    public event Action<Process> OnProcessChanged;
    public event Action<State> OnStateChanged;


    public void Init() {
        if (IsInit) return;

        methods = new List<IAccountMethod>() {
        new CustomAccountMetohd(),
        new GoogleAccountMethod(),
        new AppleAccountMethod(),
    };

        _isInit = true;
    }
    public void Release() {
        _isInit = false;
        methods.Clear();
    }

    public void ShowMethods() {
        for (int i = 0; i < methods.Count; i++) {
            var method = methods[i];

            // AccountUI
            // GetObj
            // InitMethod();
        }
    }
}

public interface IAccountMethod
{
    public Sprite Icon { get; }
    public bool IsSignedUp { get; }

    Task<AccountResult> SignInAsync();
    Task<AccountResult> SignUpAsync();
}

public class GoogleAccountMethod : IAccountMethod
{
    public Sprite Icon => Icon;
    public bool IsSignedUp => true;


    public Task<AccountResult> SignInAsync() {
        throw new System.NotImplementedException();
    }

    public Task<AccountResult> SignUpAsync() {
        throw new System.NotImplementedException();
    }
}

public class AppleAccountMethod : IAccountMethod
{
    public Sprite Icon => throw new NotImplementedException();

    public bool IsSignedUp => throw new NotImplementedException();

    public Task<AccountResult> SignInAsync() {
        throw new System.NotImplementedException();
    }

    public Task<AccountResult> SignUpAsync() {
        throw new System.NotImplementedException();
    }
}

public class CustomAccountMetohd : IAccountMethod
{
    public Sprite Icon => throw new NotImplementedException();

    public bool IsSignedUp => throw new NotImplementedException();

    public Task<AccountResult> SignInAsync() {
        throw new System.NotImplementedException();
    }

    public Task<AccountResult> SignUpAsync() {
        throw new System.NotImplementedException();
    }
}

public class AccountResult
{
    public bool IsSuccess { get; private set; }
    public string ErrorMessage { get; private set; }
    public UserData User { get; private set; }

    public static AccountResult Success(UserData user) =>
        new AccountResult { IsSuccess = true, User = user };

    public static AccountResult Failure(string error) =>
        new AccountResult { IsSuccess = false, ErrorMessage = error };
}

public class UserData
{
    public string UserId { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
