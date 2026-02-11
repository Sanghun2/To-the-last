using UnityEngine;

public class ShowUIAction : ActionBase<Structure>
{
    public ShowUIAction(Structure structureSD) {
        SetParameter(structureSD);
    }

    public override void Execute() {
        throw new System.NotImplementedException();
    }
}
