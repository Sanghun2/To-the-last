using BilliotGames;
using UnityEngine;

public sealed class StructureContextBuilderContainer : TypeRegistry<StructureDataBase, StructureContextBuilderBase>
{
    public StructureContextBuilderContainer() {
        Register<ProductionStructureData>(new ProductionStructureContextBuilder());
        Register<UtilityStructureData>(new UtilityStructureContextBuilder());
        Register<SpecialStructureData>(new SpecialStructureContextBuilder());
    }
}
