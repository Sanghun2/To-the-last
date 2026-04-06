using UnityEngine;

public abstract class NPCBase
{
    public string ID { get; }

    public NPCBase(string npcID) {
        ID = npcID;
    }
}
