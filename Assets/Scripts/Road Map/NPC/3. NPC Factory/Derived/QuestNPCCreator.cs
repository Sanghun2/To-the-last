using UnityEngine;

public class QuestNPCCreator : NPCCreatorBase<QuestNPCData, QuestNPC>
{
    public override QuestNPC CreateNPC(QuestNPCData data) {
        return new QuestNPC(data);
    }
}
