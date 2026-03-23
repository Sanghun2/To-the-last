using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
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

    private string chainID;
    private List<Process> processList = new List<Process>();
    private int currentProcessIndex;

    public ProcessChain(string groupID) {
        this.chainID = groupID;
    }

    public event Action OnChainCompleted;
    public event Action OnChainCanceled;

    public void ResetIndex() {
        currentProcessIndex = 0;
    }

    public ProcessChain AddProcess(Process process) {
        processList.Add(process);
        return this;
    }

    public async UniTask<bool> TryExecuteNextProcess(CancellationToken cancellationToken) {
        var result = TryGetNextProcess(out Process nextProcess);

        cancellationToken.ThrowIfCancellationRequested();
        if (nextProcess != null) {
            //var context = ProcessContext();
            ProcessContext context = nextProcess.BuildContext();
            await nextProcess.ExecuteProcessAsync(context, cancellationToken);
        }

        InvokeEvent(result);
        return result == Result.InProgress;
    }
    public async UniTask<bool> TryExecutePrevProcess(ProcessContext context, CancellationToken cancellationToken) {
        var result = TryGetPrevProcess(out Process prevProcess);

        cancellationToken.ThrowIfCancellationRequested();
        if (prevProcess != null) {
            await prevProcess.ExecuteProcessAsync(context, cancellationToken);
        }

        InvokeEvent(result);
        return result == Result.InProgress;
    }


    private Result TryGetNextProcess(out Process process) {
        var targetIndex = currentProcessIndex + 1;
        if (TryGetProcess(targetIndex, out process)) {
            ++currentProcessIndex;
            return Result.InProgress;
        }

        return Result.Completed;
    }
    private Result TryGetPrevProcess(out Process process) {
        var targetIndex = currentProcessIndex - 1;
        if (TryGetProcess(targetIndex, out process)) {
            --currentProcessIndex;
            return Result.InProgress;
        }

        return Result.Canceled;
    }
    private bool TryGetProcess(int index, out Process process) {
        process = null;
        //index = Mathf.Clamp(index, 0, processList.Count);
        if (0 <= index && index < processList.Count) {
            process = processList[index];
            return true;
        }

        Debug.Log("<color=yellow>out of process index</color>");
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
