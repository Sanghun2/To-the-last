using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public sealed class QuestManager : IInitializable
{
    private Dictionary<string, Quest> activeQuests = new Dictionary<string, Quest>();

    public bool IsInit => _isInit;
    private bool _isInit;

    public void Init() {
        if (IsInit) return;

        _isInit = true;
    }

    public void Release() {
        _isInit = false;
    }
}
