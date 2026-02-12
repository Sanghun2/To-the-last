using System;
using BilliotGames;
using UnityEngine;

public class ShowUIAction : ActionBase<Structure>
{
    public ShowUIAction(Structure structureSD) {
        SetParameter(structureSD);
    }

    public override void Execute() {
        Type uiType = parameter.StructureSD.GetUIType();
        if (IsMatched(uiType, typeof(CraftStructureUI).ToString())) {
            var ui = Managers.UI.OpenUI<CraftStructureUI>();
            ui.SetTitleText(parameter.StructureSD);
        }
        else if (IsMatched(uiType, typeof(UtilityStructureUI).ToString())) {
            Managers.UI.OpenUI<UtilityStructureUI>();
        }
    }

    private bool IsMatched(Type uiType, string typeName) {
        return uiType.Name.Equals(typeName);
    }
}
