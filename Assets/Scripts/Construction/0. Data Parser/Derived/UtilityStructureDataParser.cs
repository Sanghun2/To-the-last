using UnityEngine;

public class UtilityStructureDataParser : StructureDataParserBase<UtilityStructureSD, UtilityStructureData>
{
    public override UtilityStructureData ParseData(UtilityStructureSD structureSD) {
        return new UtilityStructureData(
            structureSD.ID,
            structureSD.FirstCategory,
            structureSD.DisplayText,
            structureSD.Image,
            structureSD.ConstructionTime,
            structureSD.Requirements,
            structureSD.ContentList,
            structureSD.DefaultExecitionButtonText
            );
    }
}
