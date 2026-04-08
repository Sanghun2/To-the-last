using BilliotGames;
using UnityEngine;

public class SelectionDataParserContainer : TypeRegistry<SelectionSDBase, SelectionDataParserBase>
{
    public SelectionDataParserContainer() {
        Register<LootSelectionSD>(new LootSelectionDataParser());
    }
}
