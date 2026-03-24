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



    public void StartProcess(Define.FlowType type) {
        StartProcess(type.ToString());
    }
    public void StartProcess(string chainID) {
        if (!TryGetChain(chainID, out ProcessChain chain)) return;

        ClearCurrentProcess();
        currentChainID = chainID;
        Debug.Log($"<color=cyan>[Test] ({chainID}) process started</color>");
        chain.TryExecuteCurrentProcess();
    }
    public void ExecuteNextProcess() {
        if (!TryGetChain(currentChainID, out var chain)) return;

        chain.TryExecuteNextProcess();
    }
    public void ExecutePrevProcess() {
        if (!TryGetChain(currentChainID, out var chain)) return;

        chain.TryExecutePrevProcess();
    }

    private bool TryGetChain(string chainID, out ProcessChain chain) {
        if (string.IsNullOrEmpty(chainID)) { Debug.LogError($"chain id is empty"); chain = null; return false; }
        return chainDict.TryGetValue(chainID, out chain);
    }

    #region Init

    public void Init() {
        if (IsInit) return;

        var loginChain = new ProcessChain(
            Define.FlowType.LogIn.ToString(),
            null,
            null);

        var gamePrepareChain = new ProcessChain(
            Define.FlowType.BootStrapGame.ToString(),
            Managers.BootStrap.CompleteBootStrap,
            Managers.BootStrap.CancelBootStrap);

        gamePrepareChain
            .AddProcess(new TraitSelectProcess(new TraitSelectProcessCotnextBuilder()))
            .AddProcess(new CharacterSelectProcess(new CharacterSelectProcessContextBuilder()));

        chainDict.Clear();
        chainDict.Add(Define.FlowType.LogIn.ToString(), loginChain);
        chainDict.Add(Define.FlowType.BootStrapGame.ToString(), gamePrepareChain);

        _isInit = true;
    }

    public void CompleteCurrentProcess() {
        if (TryGetChain(currentChainID, out ProcessChain chain)) {
            if (chain.CurrentProcess.CurrentState == Process.State.InProgress) {
                chain.CurrentProcess.CompleteProcess();

                ExecuteNextProcess();
            }
        }
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
