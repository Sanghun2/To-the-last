using UnityEngine;

public class UtilityStructureContextBuilder : StructureContextBuilderBase<UtilityStructureData, UtilityStructureContext>
{
    public override bool TryBuildContext(UtilityStructureData structureData, out UtilityStructureContext structureContext) {
        structureContext = new UtilityStructureContext(structureData);
        return true;
    }
}
