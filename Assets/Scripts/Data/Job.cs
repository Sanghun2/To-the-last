using System;
using UnityEngine;

[Serializable]
public class Job
{
    public enum State
    {
        Wait,
        Running,
        Completed,
    }

    public float ProgressRate => currentMinutes / totalMinutes;
    public float TotalSeconds => totalMinutes * 60;
    public int TotalMinutes => totalMinutes;
    public State CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
        }
    }


    [SerializeField] protected int currentMinutes;
    [SerializeField] protected int totalMinutes;
    private State _currentState;

    [NonSerialized] private Action completeCallback;
    [NonSerialized] private Action<float, float> progressCallback;

    public Job(int totalMinutes, Action<float, float> progressCallback = null, Action callback = null) {
        this.totalMinutes = totalMinutes;
        this.completeCallback = callback;
        this.progressCallback = progressCallback;
    }

    public void ChangeMinutes(int deltaMinutes) {
        if (CurrentState == State.Wait) return;

        currentMinutes += deltaMinutes;
        progressCallback?.Invoke(currentMinutes, totalMinutes);
        if (Mathf.Approximately(ProgressRate, 1)) {
            currentMinutes = totalMinutes;
            _currentState = State.Completed;
            completeCallback?.Invoke();
        }
    }

    public void ChangeMinutes(int day, int hour, int minute, int deltaMinutes) {
        if (CurrentState == State.Wait) return;

        currentMinutes += deltaMinutes;
        progressCallback?.Invoke(currentMinutes, totalMinutes);
        if (Mathf.Approximately(ProgressRate, 1)) {
            currentMinutes = totalMinutes;
            _currentState = State.Completed;
            completeCallback?.Invoke();
        }
    }
}

[Serializable]
public class FocusJob : Job
{
    public float Duration => duration;

    [SerializeField] protected float duration = 2.5f;

    public FocusJob(int totalMinutes, Action<float, float> progressCallback=null, Action callback=null) : base(totalMinutes, progressCallback, callback) {

    }
    public FocusJob(int totalMinutes, float duration, Action<float, float> progressCallback=null, Action callback = null) : base(totalMinutes, progressCallback, callback) {
        this.duration = duration;
    }
}
