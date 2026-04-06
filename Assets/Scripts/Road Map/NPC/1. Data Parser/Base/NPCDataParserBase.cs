using System;
using UnityEngine;

public abstract class NPCDataParserBase
{
    public abstract NPCDataBase ParseData(NPCSDBase npcSD);
}

public abstract class NPCDataParserBase<TSD, TData> : NPCDataParserBase
    where TSD : NPCSDBase
    where TData : NPCDataBase
{
    public override NPCDataBase ParseData(NPCSDBase npcSD) {
        if (npcSD is TSD tsd) {
            return ParseData(tsd);
        }

        Debug.LogError($"<color=red>({npcSD}) is not type of ({typeof(TSD)})</color>");
        return null;
    }

    public abstract TData ParseData(TSD npcSD);
}
