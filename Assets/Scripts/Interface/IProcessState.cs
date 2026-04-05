using UnityEngine;

public interface IProcessState
{
    Process.State ProcessState { get; set; }
}