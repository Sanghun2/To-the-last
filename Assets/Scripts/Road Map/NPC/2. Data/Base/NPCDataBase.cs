using UnityEngine;

public abstract class NPCDataBase
{
    public string ID { get; }

    public NPCDataBase(string npcID) {
        ID = npcID;
    }
}
