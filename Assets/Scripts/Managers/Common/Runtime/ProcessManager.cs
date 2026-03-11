using System.Collections.Generic;
using UnityEngine;

public sealed class ProcessManager
{

}

public class ProcessChain
{
    public enum State {
        Wait,
        InProgress,
        Complete,
    }

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

    public ProcessChain AddProcess(Process processs) {
        processList.Add(processs);
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
        if (index < 0) { Debug.LogError($"<color=red>index less than 0</color>"); return false; }
        if (0 <= index && index < processList.Count) {
            process = processList[index];
            return true;
        }

        return false;
    }
}

public class Process
{

}
public class Process<TContext> : Process where TContext : ProcessContext
{

}

public class ProcessContext
{

}