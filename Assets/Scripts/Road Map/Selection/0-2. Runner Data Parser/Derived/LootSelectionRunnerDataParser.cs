using UnityEngine;

public class LootSelectionRunnerDataParser : SelectionRunnerDataParserBase<LootSelectionRunnerSD, LootSelectionRunnerData>
{
    public override LootSelectionRunnerData ParseRunnerData(LootSelectionRunnerSD tsd, int requireMinutes) {
        return new LootSelectionRunnerData(tsd, requireMinutes);
    }
}
