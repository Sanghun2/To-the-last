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
        if (IsMatched(uiType, typeof(CraftStructureUI))) {
            Managers.UI.OpenUI<CraftStructureUI>();
        }
        else if (IsMatched(uiType, typeof(UtilityStructureUI))) {
            Managers.UI.OpenUI<UtilityStructureUI>();
        }
    }

    private bool IsMatched(Type uiType, Type uiBase) {
        return uiType.Name.Equals(uiBase.GetType().Name);
    }
}
