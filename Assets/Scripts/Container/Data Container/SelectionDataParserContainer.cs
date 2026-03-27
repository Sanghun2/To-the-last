using BillotGames;
using UnityEngine;

public class SelectionDataParserContainer : TypeRegistry<SelectionSD, SelectionDataParserBase>
{
    public SelectionDataParserContainer() {
        Register<LootSelectionSD>(new LootSelectionDataParser());
    }
}
