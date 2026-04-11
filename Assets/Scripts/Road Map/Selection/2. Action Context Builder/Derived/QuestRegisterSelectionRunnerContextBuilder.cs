using UnityEngine;

public class QuestRegisterSelectionRunnerContextBuilder
    : SelectionRunnerContextBuilderBase<QuestRegisterSelectionRunnerData, QuestRegisterSelectionRunnerContext>
{
    public override QuestRegisterSelectionRunnerContext BuildActionContext(QuestRegisterSelectionRunnerData data) {
        return new QuestRegisterSelectionRunnerContext(0);
    }
}
