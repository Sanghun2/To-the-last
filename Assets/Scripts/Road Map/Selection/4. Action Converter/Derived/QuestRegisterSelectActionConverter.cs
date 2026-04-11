using System;
using UnityEngine;

public class QuestRegisterSelectActionConverter : SelectActionConverterBase<QuestRegisterSelectionRunnerContext>
{
    protected override Action SelectAction(QuestRegisterSelectionRunnerContext context) {
        return () => {
            Managers.NPC.RegisterNPC(new QuestNPC(context.NPCData));
        };
    }
}
