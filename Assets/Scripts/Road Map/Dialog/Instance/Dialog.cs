using System;
using UnityEngine;

public class Dialog
{
    public enum State {
        Waiting,
        InProgress,
        Completed,
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

    public string DialogID => dialogID;
    public DialogPageData CurrentPage => currentProgress <= maxProgress ? bookData.Pages[currentProgress] : null;
    public int CurrentProgress => currentProgress;

    public string ID => bookData.ID;

    [SerializeField] State currentState;
    [SerializeField] string dialogID;
    private int currentProgress;
    private int maxProgress;
    private DialogBookData bookData;

    public event Action<State, State> OnStateChanged;
    public event Action<int, DialogPageData> OnPageChanged;
    public event Action<Dialog> OnDialogCompleted;


    public Dialog(DialogBookData dialogBookData) { // dialog book data로 변환 필요
        this.bookData = dialogBookData;
        dialogID = dialogBookData.ID;
        currentProgress = 0;
        maxProgress = dialogBookData.Pages.Count;
        currentState = State.Waiting;
    }

    public void JumpToNextPage() {
        int nextProgress = currentProgress + 1;
        if (nextProgress < maxProgress) {
            currentProgress = nextProgress;
            OnPageChanged?.Invoke(currentProgress, CurrentPage);
            return;
        }

        OnDialogCompleted?.Invoke(this);
    }
}
