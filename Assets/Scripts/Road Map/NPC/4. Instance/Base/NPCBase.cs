using System;
using UnityEngine;

public abstract class NPCBase
{
    public string ID { get; }

    public NPCBase(string npcID) {
        ID = npcID;
    }


    public abstract void InitNPC();
    public abstract void ActiveNPC();
    public abstract void ReleaseNPC();
}
