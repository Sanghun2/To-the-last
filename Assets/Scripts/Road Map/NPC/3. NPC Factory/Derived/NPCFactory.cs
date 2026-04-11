using UnityEngine;

public class NPCFactory : NPCFactoryBase<QuestNPCData, QuestNPC>
{
    public override QuestNPC CreateNPC(QuestNPCData data) {
        return new QuestNPC(data);
    }
}
