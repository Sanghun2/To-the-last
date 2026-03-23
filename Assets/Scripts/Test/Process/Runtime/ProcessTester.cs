using UnityEngine;

public class ProcessTester : MonoBehaviour
{
    [SerializeField] Define.FlowType testChainType;

    public void StartChain() {
        Managers.Process.StartProcess(testChainType.ToString());
    }
}
