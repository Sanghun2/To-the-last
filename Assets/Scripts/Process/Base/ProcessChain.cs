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
    public Process CurrentProcess => currentProcess;
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
    private List<Process> processList = new List<Process>();
    private Process currentProcess;
    private int _currentProcessIndex;
    private Action onChainCanceled;
    private Action onChainCompleted;

    public ProcessChain(string groupID, Action onChainCompleted, Action onChainCanceled) {
        this.chainID = groupID;
        this.onChainCompleted = onChainCompleted;
        this.onChainCanceled = onChainCanceled;

        OnChainCompleted += onChainCompleted;
        OnChainCanceled += onChainCanceled;
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

    public ProcessChain AddProcess(Process process) {
        processList.Add(process);
        return this;
    }

    public bool TryExecuteProcess(Process process) {
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
        var result = TryGetProcess(out Process targetProcess);

        if (targetProcess != null) {
            TryExecuteProcess(targetProcess);
            return true;
        }

        return false;
    }

    public bool TryExecuteNextProcess() {
        ClearCurrentProcess();

        if (TryGetNextProcess(out Process nextProcess)) {
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

        if (TryGetPrevProcess(out Process prevProcess)) {
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

    private Result TryGetProcess(out Process process) {
        if (TryGetProcess(_currentProcessIndex, out process)) {
            return Result.InProgress;
        }

        return Result.Completed;
    }
    private bool TryGetNextProcess(out Process process) {
        var targetIndex = _currentProcessIndex + 1;
        if (TryGetProcess(targetIndex, out process)) {
            return true;
        }

        return false;
    }
    private bool TryGetPrevProcess(out Process process) {
        var targetIndex = _currentProcessIndex - 1;
        if (TryGetProcess(targetIndex, out process)) {
            return true;
        }

        return false;
    }
    private bool TryGetProcess(int index, out Process process) {
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
