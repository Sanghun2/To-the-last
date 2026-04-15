using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProcessTester : MonoBehaviour
{
    [SerializeField] Define.FlowType testChainType;

    public void StartProcess() {
        Managers.Process.TryStartProcess(testChainType);
    }
    public void NextProcess() {
        Managers.Process.TryExecuteNextProcess();
    }

    public void PrevProcess() {
        Managers.Process.TryExecutePrevProcess();
    }
}
