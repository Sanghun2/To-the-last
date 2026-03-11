using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class ProcessManager
{
    private Dictionary<string, ProcessChain> processChainDict = new();

    public void RegisterChain(string chainID, ProcessChain processChain) {
        processChainDict[chainID] = processChain;
        Debug.Log($"[Test] ({chainID}) chain registered");
    }
    public void RegisterChain(ProcessChain processChain) {
        RegisterChain(processChain.GroupID, processChain);
    }


    public void UnregisterChain(string chainID) {
        processChainDict.Remove(chainID);
    }
    public async UniTask ExecuteProcessChain(string chainID, ProcessContext context, CancellationToken cancellationToken) {
        if (!TryGetChain(chainID, out ProcessChain chain)) { return; }

        chain.ResetIndex();
        while (chain.TryGetProcess(out Process process)) {
            cancellationToken.ThrowIfCancellationRequested();
            await process.ExecuteAsync(context, cancellationToken);
        }
    }

    private bool TryGetChain(string processID, out ProcessChain chain) {
        if (processChainDict.TryGetValue(processID, out chain)) {
            return true;
        }

        Debug.LogError($"<color=red>Chain of ID ({processID}) is not exist</color>");
        return false;
    }
}


public sealed class ProcessChain
{
    public string GroupID => groupID;

    [SerializeField] string groupID;
    private List<Process> processList = new List<Process>();
    private int currentProcessIndex;

    public ProcessChain(string groupID) {
        this.groupID = groupID;
    }

    public void ResetIndex() {
        currentProcessIndex = 0;
    }

    public ProcessChain AddProcess(Process process) {
        processList.Add(process);
        return this;
    }

    public bool TryGetProcess(out Process process) {
        if (TryGetProcess(currentProcessIndex, out process)) {
            ++currentProcessIndex;
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

        return false;
    }
}


public abstract class Process
{
    public abstract UniTask ExecuteAsync(ProcessContext context, CancellationToken cancellationToken);
}
public abstract class Process<TContext> : Process where TContext : ProcessContext
{
    public abstract UniTask ExecuteAsync(TContext context, CancellationToken cancellationToken);
    public override async UniTask ExecuteAsync(ProcessContext context, CancellationToken cancellationToken) {
        if (context is TContext tContext) {
            await ExecuteAsync(tContext, cancellationToken);
        }
        else {
            Debug.LogError($"<color=red>context type ({context.GetType()}) is not ({typeof(TContext)})</color>");            
        }
    }
}


public abstract class ProcessContext
{

}