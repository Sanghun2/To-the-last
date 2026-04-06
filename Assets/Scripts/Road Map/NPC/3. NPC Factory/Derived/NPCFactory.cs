using UnityEngine;

public class NPCFactory : NPCFactoryBase<NPCData, NPC>
{
    public override NPC CreateNPC(NPCData data) {
        return new NPC(data);
    }
}
