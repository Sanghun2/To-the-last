using UnityEngine;

public class ShowUIAction : ActionBase<StructureSD>
{
    public ShowUIAction(StructureSD structureSD) {
        SetParameter(structureSD);
    }

    public override void Execute() {
        throw new System.NotImplementedException();
    }
}
