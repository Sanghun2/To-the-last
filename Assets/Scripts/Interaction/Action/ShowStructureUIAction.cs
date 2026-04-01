using System;
using BilliotGames;
using UnityEngine;

public class ShowStructureUIAction : ActionBase<Structure>
{
    public ShowStructureUIAction(Structure structureSD) {
        SetParameter(structureSD);
    }

    public override void Execute() {
        OpenStructureUI();
    }

    private void OpenStructureUI() {
        Structure structure = parameter;
        Managers.Structure.SetStructure(structure);
        StructureContextBase structureContext = parameter.StructureContext;
        StructureUIBase structureUI = structureContext.OpenStructureUI();
        structureUI.SetTitleText(structureContext.DisplayText);

    }
}
