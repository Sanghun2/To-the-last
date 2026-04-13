using UnityEngine;

public class SpecialStructureDataParser : StructureDataParserBase<SpecialStructureSD, SpecialStructureData>
{
    public override SpecialStructureData ParseData(SpecialStructureSD structureSD) {
        return new SpecialStructureData(
            structureSD.ID,
            structureSD.FirstCategory,
            structureSD.DisplayName,
            structureSD.Image,
            structureSD.ConstructionTime,
            structureSD.Requirements,
            structureSD.DefaultExecitionButtonText,
            structureSD.StructureType
            );
    }
}
