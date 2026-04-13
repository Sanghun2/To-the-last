using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class NPCManager : IInitializable
{
    public bool IsInit => _isInit;
    public TradeNPCMarkerUIContainer tradeNPCMarkerUIContainer
    {
        get
        {
            if(_tradeNPCMarkerUIContainer == null) {
                _tradeNPCMarkerUIContainer = GameObject.FindAnyObjectByType<TradeNPCMarkerUIContainer>(FindObjectsInactive.Exclude);
            }

            return _tradeNPCMarkerUIContainer;
        }
    }

    private Dictionary<string, NPCBase> npcDict = new Dictionary<string, NPCBase>();

    private NPCDataParserContainer npcDataParserContainer = new NPCDataParserContainer();
    private NPCCreatorContainer npcCreatorContainer = new NPCCreatorContainer();
    private bool _isInit;
    private TradeNPCMarkerUIContainer _tradeNPCMarkerUIContainer;

    public void Init() {
        if (IsInit) return;

        _isInit = true;
    }

    public bool TryActivateNPC(NPCSDBase npcSD, out NPCBase targetNPC) {
        targetNPC = CreateNPC(npcSD);
        return TryActivateNPC(targetNPC);
    }
    public bool TryActivateNPC(NPCBase npcBase) {
        if (npcBase == null) { Debug.LogError($"npc is null"); return false; }

        npcBase.InitNPC();
        npcBase.ActiveNPC();
        npcDict[npcBase.ID] = npcBase;
        return true;
    }
    public bool TryInactivateNPC(string npcID) {
        if (npcDict.TryGetValue(npcID, out var targetNPC)) {
            targetNPC.ReleaseNPC();
            npcDict.Remove(npcID);
            return true;
        }

        return false;
    }

    public bool TryGetNPC(string npcID, out NPCBase npc) {
        return npcDict.TryGetValue(npcID, out npc);
    }

    public void Release() {
        throw new NotImplementedException();
    }



    private NPCBase CreateNPC(NPCSDBase npcSD) {
        if (!npcDataParserContainer.TryGet(npcSD, out var parser)) { Debug.LogError($"<color=red>({npcSD.GetType()}) data parser is null</color>"); return null; }
        NPCDataBase npcData = parser.ParseData(npcSD);

        return CreateNPC(npcData);
    }
    private NPCBase CreateNPC(NPCDataBase npcData) {
        if (!npcCreatorContainer.TryGet(npcData, out var factory)) { Debug.LogError($"({npcData.GetType()}) factory is not exist"); return null; }
        return factory.CreateNPC(npcData);
    }
}
