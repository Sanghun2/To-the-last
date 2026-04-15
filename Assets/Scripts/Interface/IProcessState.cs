using UnityEngine;

public interface IProcessState
{
    ProcessBase.State ProcessState { get; set; }
}