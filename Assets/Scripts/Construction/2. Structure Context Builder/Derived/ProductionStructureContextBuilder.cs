using UnityEngine;

public class ProductionStructureContextBuilder : StructureContextBuilderBase<ProductionStructureData, ProductionStructureContext>
{
    public override bool TryBuildContext(ProductionStructureData structureData, out ProductionStructureContext structureContext) {
        structureContext = new ProductionStructureContext(structureData);
        return true;
    }
}
