using UnityEngine;

public class QuestRegisterSelectionRunnerDataParser : SelectionRunnerDataParserBase<QuestRegisterSelectionRunnerSD, QuestRegisterSelectionRunnerData>
{
    public override QuestRegisterSelectionRunnerData ParseRunnerData(QuestRegisterSelectionRunnerSD tsd, int requireMinutes) {
        return new QuestRegisterSelectionRunnerData();
    }
}
