using UnityEngine;

public class SpecialStructureContextBuilder : StructureContextBuilderBase<SpecialStructureData, SpecialStructureContext>
{
    public override bool TryBuildContext(SpecialStructureData structureData, out SpecialStructureContext structureContext) {
        structureContext = new SpecialStructureContext(structureData);
        return true;
    }
}
