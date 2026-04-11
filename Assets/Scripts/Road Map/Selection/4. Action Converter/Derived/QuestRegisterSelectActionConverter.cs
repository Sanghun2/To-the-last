using System;
using UnityEngine;

public class QuestRegisterSelectActionConverter : SelectActionConverterBase<QuestRegisterSelectionRunnerContext>
{
    protected override Action SelectAction(QuestRegisterSelectionRunnerContext context) {
        return () => {
            if (Managers.NPC.TryActivateNPC(new QuestNPC(context.NPCData))) {
                
            }
        };
    }
}
