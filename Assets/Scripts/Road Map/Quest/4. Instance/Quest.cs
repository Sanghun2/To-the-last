using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Quest
{
    public enum Type
    {
        Main,
        Sub,
        Daily,
        Achievement,
    }
    public enum State
    {
        Wait,
        InProgress,
        Completed,
        Canceled,
    }

    public string ID => Data.ID;
    public QuestData Data => questData;
    public IReadOnlyList<Task> TaskList => taskList;
    public Task CurrentTask => taskList[currentProgressIndex];
    private int LastProgressIndex => taskList.Count - 1;
    public State CurrentState
    {
        get => _currentState;
        private set
        {
            var prevState = _currentState;
            _currentState = value;
            if (_currentState != prevState) {
                OnStateChanged?.Invoke(this, _currentState);
            }
        }
    }


    [SerializeField] QuestData questData;
    [SerializeField] List<Task> taskList;
    [SerializeField] State _currentState;
    [SerializeField] int currentProgressIndex;

    public event Action<Quest, State> OnStateChanged;
    public event Action<Quest> OnCanceled;

    public Quest(QuestData data) {
        questData = data;
        taskList = data.TaskInfos.Select(t => new Task(t)).ToList();
        _currentState = State.Wait;
    }

    public void StartQuest() {
        CurrentState = State.InProgress;
        CurrentTask.StartTask();
    }
    public bool TryCompleteCurrentTask(bool continueNextTask=true) {
        if (CurrentState != State.InProgress) return false;

        var task = CurrentTask;
        if (task.CurrentState != Task.State.CanComplete) return false;

        if (CurrentTask.TryComplete()) {
            if (currentProgressIndex == LastProgressIndex) CurrentState = State.Completed;
            else {
                currentProgressIndex = Mathf.Clamp(currentProgressIndex + 1, 0, LastProgressIndex);
                if (continueNextTask) CurrentTask.StartTask();
            }

            return true;
        }

        return false;
    }

    public void Cancel() {
        CurrentState = State.Canceled;
        OnCanceled?.Invoke(this);
    }
}
