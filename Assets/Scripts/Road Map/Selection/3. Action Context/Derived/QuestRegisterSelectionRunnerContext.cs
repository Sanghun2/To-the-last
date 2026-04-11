using UnityEngine;

public class QuestRegisterSelectionRunnerContext : SelectionRunnerContextBase
{
    public QuestRegisterSelectionRunnerContext(int jobDuration) : base(jobDuration) {
    }

    public QuestNPCData NPCData { get; }
}
