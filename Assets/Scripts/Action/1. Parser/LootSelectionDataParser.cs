using System.Collections.Generic;
using UnityEngine;

public class LootSelectionDataParser : SelectionDataParserBase<LootSelectionSD, LootSelectionData>
{
    public override LootSelectionData Parse(LootSelectionSD sd) {
        return new LootSelectionData(
            sd.LootItemDataList
            );
    }
}
