using System;
using UnityEngine;

public abstract class NPCFactoryBase
{
    public abstract NPCBase CreateNPC(NPCDataBase npcData);
}

public abstract class NPCFactoryBase<TData, TInstance> : NPCFactoryBase
    where TData : NPCDataBase
    where TInstance : NPCBase
{
    public override NPCBase CreateNPC(NPCDataBase npcData) {
        if (npcData is TData data) {
            return CreateNPC(data);
        }

        Debug.LogError($"<color=red>({npcData.GetType()}) is not type of ({typeof(TData)})</color>");
        return null;
    }

    public abstract TInstance CreateNPC(TData data);
}
