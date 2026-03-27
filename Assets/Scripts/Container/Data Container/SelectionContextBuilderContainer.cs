using BillotGames;
using UnityEngine;

public class SelectionContextBuilderContainer : TypeRegistry<SelectionDataBase, SelectionContextBuilderBase>
{
    public SelectionContextBuilderContainer() {
        Register<LootSelectionData>(new LootSelectionContextBuilder());
    }
}
