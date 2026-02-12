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
            var structureSD = parameter.StructureSD;
            if (structureSD.GetType().Name.Equals(typeof(ProductionStructureSD).ToString())) {
                var ps = structureSD as ProductionStructureSD;
                ui.InitProgressUI(0,1); // 추후 현재 작업중인 내용의 progress로 변경
                ui.ShowList(ps.Prouctions);
            }
        }
        else if (IsMatched(uiType, typeof(UtilityStructureUI).ToString())) {
            Managers.UI.OpenUI<UtilityStructureUI>();
        }
    }

    private bool IsMatched(Type uiType, string typeName) {
        return uiType.Name.Equals(typeName);
    }
}
