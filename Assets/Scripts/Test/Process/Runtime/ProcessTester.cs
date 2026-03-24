using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ProcessTester : MonoBehaviour
{
    [SerializeField] Define.FlowType testChainType;

    public void StartProcess() {
        Managers.Process.StartProcess(testChainType);
    }
    public void NextProcess() {
        Managers.Process.ExecuteNextProcess();
    }

    public void PrevProcess() {
        Managers.Process.ExecutePrevProcess();
    }
}
