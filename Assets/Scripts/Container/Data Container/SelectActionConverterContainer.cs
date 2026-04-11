using BilliotGames;
using UnityEngine;

public class SelectActionConverterContainer : TypeRegistry<SelectionRunnerContextBase,  SelectActionConverterBase>
{
    public SelectActionConverterContainer() {
        Register<LootSelectionRunnerContext>(new LootSelectActionConverter());
        Register<DialogSelectionRunnerContext>(new DialogSelectActionConverter());
    }
}
