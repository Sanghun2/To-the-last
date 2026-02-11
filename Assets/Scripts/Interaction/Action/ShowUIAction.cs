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
        if (uiType.Name.Equals(typeof(CraftStructureUI).ToString())) {
            Managers.UI.OpenUI<CraftStructureUI>();
        }
    }
}
