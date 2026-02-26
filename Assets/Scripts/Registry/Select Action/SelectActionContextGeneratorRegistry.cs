using UnityEngine;

public class SelectActionContextGeneratorRegistry : TypeRegistry<SelectionSD, SelectActionContextGenerator>
{
    public SelectActionContextGeneratorRegistry() {
        Register<LootSelectionSD>(new LootSelectActionContextGenerator());
    }
}
