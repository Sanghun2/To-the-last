using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class ProcessManager : IInitializable
{
    public bool IsInit => _isInit;

    private Dictionary<string, ProcessChain> chainDict = new();
    private CancellationTokenSource cancelToken;
    private bool _isInit;


    public async UniTask StartProcess(string chainID) {
        if (!TryGetChain(chainID, out ProcessChain chain)) return;

        CancelProcess();
        chain.ResetIndex();
        cancelToken = new CancellationTokenSource();
        while (await chain.TryExecuteNextProcess(cancelToken.Token)) { }
    }

    private bool TryGetChain(string chainID, out ProcessChain chain) {
        return chainDict.TryGetValue(chainID, out chain);
    }

    #region Init

    public void Init() {
        if (IsInit) return;

        var loginChain = new ProcessChain(Define.FlowType.LogIn.ToString());
        var gamePrepareChain = new ProcessChain(Define.FlowType.PrepareGame.ToString());

        gamePrepareChain
            .AddProcess(new TraitSelectProcess(new TraitSelectProcessCotnextBuilder()))
            .AddProcess(new CharacterSelectProcess(new CharacterSelectProcessContextBuilder()));

        chainDict.Clear();
        chainDict.Add(Define.FlowType.LogIn.ToString(), loginChain);
        chainDict.Add(Define.FlowType.PrepareGame.ToString(), gamePrepareChain);

        _isInit = true;
    }
    public void CancelProcess() {
        cancelToken?.Cancel();
        cancelToken?.Dispose();
        cancelToken = null;
    }
    public void Release() {
        CancelProcess();
        chainDict.Clear();
    }

    #endregion
}
