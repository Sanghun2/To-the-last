using UnityEngine;

public class DialogSelectionRunnerDataParser : SelectionRunnerDataParserBase<DialogSelectionRunnerSD, DialogSelectionRunnerData>
{
    public override DialogSelectionRunnerData ParseRunnerData(DialogSelectionRunnerSD tsd, int requireMinutes) {
        return new DialogSelectionRunnerData();
    }
}
