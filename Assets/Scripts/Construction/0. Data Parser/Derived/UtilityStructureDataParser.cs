using UnityEngine;

public class UtilityStructureDataParser : StructureDataParserBase<UtilityStructureSD, UtilityStructureData>
{
    public override UtilityStructureData ParseData(UtilityStructureSD structureSD) {
        return new UtilityStructureData(
            structureSD.ID,
            structureSD.DisplayText,
            structureSD.Image,
            structureSD.ConstructionTime,
            structureSD.RequirementItems,
            structureSD.ContentList
            );
    }
}
