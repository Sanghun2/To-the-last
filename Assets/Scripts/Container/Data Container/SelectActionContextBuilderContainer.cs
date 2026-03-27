using BillotGames;
using UnityEngine;

public class SelectActionContextBuilderContainer : TypeRegistry<SelectionDataBase, SelectActionContextBuilderBase>
{
    public SelectActionContextBuilderContainer() {
        Register<LootSelectionData>(new LootSelectActionContextBuilder());
    }
}
