using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class Quest
{
    public enum Type {
        Main,
        Sub,
        Daily,
        Achievement,
    }
    public enum State {
        Wait,
        InProgress,
        Completed,
    }

    public QuestData Data => questData;
    public IReadOnlyList<Task> TaskList => taskList;
    public Task CurrentTask => taskList[currentProgressIndex];
    private int LastProgressIndex => taskList.Count - 1;
    private State CurrentState
    {
        get => _currentState;
        set
        {
            var prevState = _currentState;
            _currentState = value;
            if(_currentState != prevState) {
                OnStateChanged?.Invoke(this, _currentState);
            }
        }
    }


    [SerializeField] QuestData questData;
    [SerializeField] List<Task> taskList;
    private State _currentState;
    private int currentProgressIndex;

    public event Action<Quest, State> OnStateChanged;

    public Quest(QuestData data) {
        questData = data;
        taskList = data.TaskInfos.Select(t => new Task(t)).ToList();
        _currentState = State.Wait;
    }

    public void CompleteCurrentTask() {
        if (CurrentState == State.Completed) return;

        CurrentTask.Complete();
        currentProgressIndex = Mathf.Clamp(currentProgressIndex+1, 0, LastProgressIndex);

        if (currentProgressIndex == LastProgressIndex) CurrentState = State.Completed;
    }
}
