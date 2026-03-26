using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Process
{
    public enum State {
        Wait,
        InProgress,
        Completed,
    }

    //public AutoResetUniTaskCompletionSource CompleteSource
    //{
    //    get
    //    {
    //        if (_completionSource == null) {
    //            _completionSource = AutoResetUniTaskCompletionSource.Create();
    //        }

    //        return _completionSource;
    //    }
    //}

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

    public Process(ProcessContextBuilder contextBuilder) {
        this.contextBuilder = contextBuilder;
    }

    public virtual event Action<Process, State> OnStateChanged;

    public void ResetProcess() {
        CurrentState = State.Wait;
    }

    public void ExecuteProcess(ProcessContext context) {
        CurrentState = State.InProgress;
        //Debug.Log($"<color=cyan>[Test] process ({GetType()}) in progress</color>");
        ExecuteProcessInternalAsync(context);
    }
    protected abstract void ExecuteProcessInternalAsync(ProcessContext context);

    public void CompleteProcess() {
        CurrentState = State.Completed;
        OnComplete();
        //Debug.Log($"<color=cyan>[Test] process ({GetType()}) completed</color>");
    }

    public void Clear() {
        CurrentState = State.Wait;
        OnCleared();
        //Debug.Log($"[Test] ({GetType()}) Process Canceled");
    }

    protected abstract void OnComplete();
    protected abstract void OnCleared();

    public ProcessContext BuildContext() {
        return contextBuilder?.BuildContext();
    }
}

public abstract class Process<TContext> : Process where TContext : ProcessContext
{
    protected Process(ProcessContextBuilder<TContext> contextBuilder) : base(contextBuilder) { }

    protected abstract void OnExecuteAsync(TContext context);

    protected override void ExecuteProcessInternalAsync(ProcessContext context) {
        if (context is TContext tContext) {
            OnExecuteAsync(tContext);
        }
        else {
            Debug.LogError($"<color=red>context type mismatch</color>");
        }
    }
}