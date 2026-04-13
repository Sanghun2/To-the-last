using UnityEngine;

public class ProductionStructureDataParser : StructureDataParserBase<ProductionStructureSD, ProductionStructureData>
{
    public override ProductionStructureData ParseData(ProductionStructureSD structureSD) {
        return new ProductionStructureData(
            structureSD.ID,
            structureSD.FirstCategory,
            structureSD.DisplayName,
            structureSD.Image,
            structureSD.ConstructionTime,
            structureSD.Requirements,
            structureSD.ContentList,
            structureSD.DefaultExecitionButtonText
            );
    }
}
