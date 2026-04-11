using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class NPCManager : IInitializable
{
    public bool IsInit => _isInit;

    private Dictionary<string, NPCBase> npcDict = new Dictionary<string, NPCBase>();
    private NPCDataParserContainer npcDataParserContainer = new NPCDataParserContainer();
    private NPCCreatorContainer npcCreatorContainer = new NPCCreatorContainer();
    private bool _isInit;

    public void Init() {
        if (IsInit) return;

        _isInit = true;
    }

    public void RegisterNPC(NPCSDBase npcSD) {
        RegisterNPC(CreateNPC(npcSD));
    }
    public void RegisterNPC(NPCBase npcBase) {
        npcDict[npcBase.ID] = npcBase;
    }

    public bool TryGetNPC(string npcID, out NPCBase npc) {
        return npcDict.TryGetValue(npcID, out npc);
    }

    public void Release() {
        throw new NotImplementedException();
    }



    private NPCBase CreateNPC(NPCSDBase npcSD) {
        if (npcDataParserContainer.TryGet(npcSD, out var parser)) { Debug.LogError($"<color=red>({npcSD.GetType()}) data parser is null</color>"); return null; }
        NPCDataBase npcData = parser.ParseData(npcSD);

        return CreateNPC(npcData);
    }
    private NPCBase CreateNPC(NPCDataBase npcData) {
        if (!npcCreatorContainer.TryGet(npcData, out var factory)) { Debug.LogError($"({npcData.GetType()}) factory is not exist"); return null; }
        return factory.CreateNPC(npcData);
    }
}
