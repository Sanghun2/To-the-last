using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ProcessBase
{
    public enum State {
        Wait,
        InProgress,
        Completed,
    }

    public State CurrentState
    {
        get => _currentState;
        set
        {
            var prevState = _currentState;
            _currentState = value;
            if (_currentState != prevState) {
                OnStateChanged?.Invoke(this, _currentState);
            }
        }
    }


    protected State _currentState;
    protected ProcessContextBuilder contextBuilder;

    public ProcessBase(ProcessContextBuilder contextBuilder) {
        this.contextBuilder = contextBuilder;
    }

    public virtual event Action<ProcessBase, State> OnStateChanged;

    public void ResetProcess() {
        CurrentState = State.Wait;
    }

    public void ExecuteProcess(ProcessContext context) {
        CurrentState = State.InProgress;
        //Debug.Log($"<color=cyan>[Test] process ({GetType()}) in progress</color>");
        ExecuteProcessInternalAsync(context);
    }

    public bool TryCompleteProcess() {
        if (!CanComplete()) return false;

        CurrentState = State.Completed;
        OnComplete();
        return true;
    }
    public void Clear() {
        CurrentState = State.Wait;
        OnCleared();
        //Debug.Log($"[Test] ({GetType()}) ProcessBase Canceled");
    }

    public abstract bool CanComplete();
    protected abstract void ExecuteProcessInternalAsync(ProcessContext context);
    protected abstract void OnComplete();
    protected abstract void OnCleared();

    public ProcessContext BuildContext() {
        return contextBuilder?.BuildContext();
    }
}

public abstract class ProcessBase<TContext> : ProcessBase where TContext : ProcessContext
{
    protected ProcessBase(ProcessContextBuilder<TContext> contextBuilder) : base(contextBuilder) { }

    protected abstract void OnExecute(TContext context);

    protected override void ExecuteProcessInternalAsync(ProcessContext context) {
        if (context is TContext tContext) {
            OnExecute(tContext);
        }
        else {
            Debug.LogError($"<color=red>context type mismatch</color>");
        }
    }
}