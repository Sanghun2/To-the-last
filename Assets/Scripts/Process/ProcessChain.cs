using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Mono.Cecil;
using UnityEngine;

public sealed class ProcessChain
{
    public enum Result
    {
        None,
        Canceled,
        InProgress,
        Completed,
    }

    public string ChainID => chainID;

    public int LastProcessIndex => processList.Count - 1;
    public ProcessBase CurrentProcess => currentProcess;
    public bool IsLastProcess => _currentProcessIndex == LastProcessIndex;
    public bool IsFirstProcess => _currentProcessIndex == 0;
    public int CurrentProcessIndex
    {
        get => _currentProcessIndex;
        private set
        {
            var prevIndex = _currentProcessIndex;
            _currentProcessIndex = value;

            if (_currentProcessIndex != prevIndex) {
                OnProcessChanged?.Invoke(_currentProcessIndex, prevIndex);
            }
        }
    }

    private string chainID;
    private List<ProcessBase> processList = new List<ProcessBase>();
    private ProcessBase currentProcess;
    private int _currentProcessIndex;
    private Action onChainCanceled;
    private Action onChainCompleted;

    public ProcessChain(string groupID, Action onChainCompleted=null, Action onChainCanceled=null) {
        this.chainID = groupID;
        this.onChainCompleted = onChainCompleted;
        this.onChainCanceled = onChainCanceled;

        if (onChainCompleted != null) OnChainCompleted += onChainCompleted;
        if (onChainCanceled != null) OnChainCanceled += onChainCanceled;
    }

    public event Action OnChainCompleted;
    public event Action OnChainCanceled;
    public event Action<int, int> OnProcessChanged;

    public void ResetChain() {
        _currentProcessIndex = 0;
        currentProcess?.Clear();
        currentProcess = null;
        OnChainCompleted = null;
        OnChainCanceled = null;

        OnChainCanceled += onChainCanceled;
        OnChainCompleted += onChainCompleted;
    }

    public ProcessChain AddProcess(ProcessBase process) {
        processList.Add(process);
        return this;
    }
    public ProcessChain AddCompleteEvent(Action completeEvent) {
        OnChainCompleted -= completeEvent;
        OnChainCompleted += completeEvent;
        return this;
    }
    public ProcessChain RemoveCompleteEvent(Action completeEvent) {
        OnChainCompleted -= completeEvent;
        return this;
    }
    public ProcessChain AddCancelEvent(Action cancelEvent) {
        OnChainCanceled -= cancelEvent;
        OnChainCanceled += cancelEvent;
        return this;
    }
    public ProcessChain RemoveCancelEvent(Action cancelEvent) {
        OnChainCanceled -= cancelEvent;
        return this;
    }

    public bool TryExecuteProcess(ProcessBase process) {
        ClearCurrentProcess();
        if (process != null) {
            currentProcess = process;
            ProcessContext context = process.BuildContext();
            process.ExecuteProcess(context);
            return true;
        }

        return false;
    }
    public bool TryExecuteCurrentProcess() {
        var result = TryGetProcess(out ProcessBase targetProcess);

        if (targetProcess != null) {
            TryExecuteProcess(targetProcess);
            return true;
        }

        return false;
    }

    public bool TryExecuteNextProcess() {
        ClearCurrentProcess();

        if (TryGetNextProcess(out ProcessBase nextProcess)) {
            if (TryExecuteProcess(nextProcess)) {
                ++CurrentProcessIndex;
                return true;
            }
        }

        OnChainCompleted?.Invoke();
        return false;
    }
    public bool TryExecutePrevProcess() {
        ClearCurrentProcess();

        if (TryGetPrevProcess(out ProcessBase prevProcess)) {
            if (TryExecuteProcess(prevProcess)) {
                --CurrentProcessIndex;
                return true;
            }
        }

        OnChainCanceled?.Invoke();
        Debug.LogAssertion($"chain canceled");
        return false;
    }
    public void ClearCurrentProcess() {
        currentProcess?.Clear();
        currentProcess = null;
    }

    private Result TryGetProcess(out ProcessBase process) {
        if (TryGetProcess(_currentProcessIndex, out process)) {
            return Result.InProgress;
        }

        return Result.Completed;
    }
    private bool TryGetNextProcess(out ProcessBase process) {
        var targetIndex = _currentProcessIndex + 1;
        if (TryGetProcess(targetIndex, out process)) {
            return true;
        }

        return false;
    }
    private bool TryGetPrevProcess(out ProcessBase process) {
        var targetIndex = _currentProcessIndex - 1;
        if (TryGetProcess(targetIndex, out process)) {
            return true;
        }

        return false;
    }
    private bool TryGetProcess(int index, out ProcessBase process) {
        process = null;
        //index = Mathf.Clamp(index, 0, processList.Count);
        if (0 <= index && index < processList.Count) {
            process = processList[index];
            return true;
        }

        Debug.Log($"<color=yellow>({index}) out of process index. max? {processList.Count}</color>");
        return false;
    }

    private void InvokeEvent(Result result) {
        switch (result) {
            case Result.None:
                break;
            case Result.Canceled:
                OnChainCanceled?.Invoke();
                break;
            case Result.InProgress:
                break;
            case Result.Completed:
                OnChainCompleted?.Invoke();
                break;
            default:
                break;
        }
    }

}
