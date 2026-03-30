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
        var structure = parameter;
        var structureContext = parameter.StructureContext;
        StructureUIBase structureUI = structureContext.OpenStructureUI();
        structureUI.SetTitleText(structureContext.DisplayText);
    }

    private bool IsMatched(Type uiType, string typeName) {
        return uiType.Name.Equals(typeName);
    }
}
