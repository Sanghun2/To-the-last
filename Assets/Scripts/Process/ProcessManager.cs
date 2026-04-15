using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class ProcessManager : IInitializable
{
    public bool IsInit => _isInit;

    public ProcessChain CurrentChain => TryGetChain(currentChainID, out var chain) ? chain : null;

    private Dictionary<string, ProcessChain> chainDict = new();
    private string currentChainID;
    private bool _isInit;



    public bool TryStartProcess(Define.FlowType type) {
        return TryStartProcess(type.ToString());
    }
    public bool TryExecuteNextProcess() {
        if (!TryGetChain(currentChainID, out var chain)) return false;

        return chain.TryExecuteNextProcess();
    }
    public bool TryExecutePrevProcess() {
        if (!TryGetChain(currentChainID, out var chain)) return false;

        return chain.TryExecutePrevProcess();
    }

    private bool TryStartProcess(string chainID) {
        if (!TryGetChain(chainID, out ProcessChain chain)) return false;

        ClearCurrentProcess();
        currentChainID = chainID;
        Debug.Log($"<color=cyan>[Test] ({chainID}) process started</color>");
        return chain.TryExecuteCurrentProcess();
    }
    private bool TryGetChain(string chainID, out ProcessChain chain) {
        if (string.IsNullOrEmpty(chainID)) { Debug.LogError($"chain id is empty"); chain = null; return false; }
        return chainDict.TryGetValue(chainID, out chain);
    }

    #region Init

    public void Init() {
        if (IsInit) return;

        var loginChain = new ProcessChain(Define.FlowType.LogIn.ToString());

        // 휴먼 에러 방지하기 위해 enum으로 chain key 사용
        var gamePrepareChain = new ProcessChain(Define.FlowType.BootStrapGame.ToString()) 
            .AddCancelEvent(Managers.BootStrap.CancelBootStrap)
            .AddProcess(new CharacterSelectionProcess(new CharacterSelectProcessContextBuilder()))
            .AddProcess(new TraitSelectionProcess(new TraitSelectProcessCotnextBuilder()))
            .AddCompleteEvent(Managers.BootStrap.CompleteBootStrap);

        chainDict.Clear();
        chainDict.Add(Define.FlowType.LogIn.ToString(), loginChain);
        chainDict.Add(Define.FlowType.BootStrapGame.ToString(), gamePrepareChain);

        _isInit = true;
    }

    public bool TryCompleteCurrentProcess() {
        if (TryGetChain(currentChainID, out ProcessChain chain)) {
            if (chain.CurrentProcess.CurrentState == ProcessBase.State.InProgress) {
                bool result = chain.CurrentProcess.TryCompleteProcess();

                TryExecuteNextProcess(); // 다음 process 실행. if false == completed state
                return result;
            }
        }

        return false;
    }
    private void ClearCurrentProcess() {
        if (string.IsNullOrEmpty(currentChainID)) return;

        if (TryGetChain(currentChainID, out var chain)) {
            chain.ClearCurrentProcess();
        }

        currentChainID = null;
    }
    public void Release() {
        ClearCurrentProcess();
        chainDict.Clear();
    }


    #endregion
}
