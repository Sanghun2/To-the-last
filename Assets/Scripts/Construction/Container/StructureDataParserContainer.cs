using BilliotGames;
using UnityEngine;

public sealed class StructureDataParserContainer : TypeRegistry<StructureSDBase, StructureDataParserBase>
{
    public StructureDataParserContainer() {
        Register<ProductionStructureSD>(new ProductionStructureDataParser());
        Register<UtilityStructureSD>(new UtilityStructureDataParser());
        Register<SpecialStructureSD>(new SpecialStructureDataParser());
    }
}
