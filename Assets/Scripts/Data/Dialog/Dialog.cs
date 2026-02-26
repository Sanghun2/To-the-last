using System;
using UnityEngine;

public class Dialog
{
    public enum State {
        Idle,
        Running,
        Done
    }

    public State CurrentState
    {
        get => currentState;
        set
        {
            var prevState = currentState;
            currentState = value;
            if (currentState != prevState) {
                OnStateChanged?.Invoke(currentState, prevState);
            }
        }
    }

    public DialogSD DialogSD => dialogSD;
    public string DialogID => dialogID;

    [SerializeField] State currentState;
    [SerializeField] string dialogID;
    private DialogSD dialogSD;

    public event Action<State, State> OnStateChanged;

    public Dialog(DialogSD dialogSD) {
        this.dialogSD = dialogSD;
        dialogID = dialogSD.ID;
    }

    public Dialog(DialogSD dialogSD, State state) : this(dialogSD) {
        currentState = state;
    }
}
