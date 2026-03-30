using BilliotGames;
using UnityEngine;

public class StructureDataParserContainer : TypeRegistry<StructureSD, StructureDataParserBase>
{
    public StructureDataParserContainer() {
        Register<ProductionStructureSD>(new ProductionStructureDataParser());
        Register<UtilityStructureSD>(new UtilityStructureDataParser());
    }
}
