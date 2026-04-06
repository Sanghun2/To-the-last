using System;
using BilliotGames;
using UnityEngine;

public class SpecialStructureContext : StructureContextBase<SpecialStructureData>
{
    private Structure.SpecialStructureType structureType;

    public SpecialStructureContext(SpecialStructureData data) : base(data) {
        structureType = data.StructureType;
    }

    public override StructureUIBase OpenStructureUI() {
        Type uiType = GetUIType(structureType);
        var ui = Managers.UI.GetUI(uiType);
        if (ui is StructureUIBase structureUI) {
            if (structureUI.IsOpened) return structureUI;

            Managers.UI.OpenUI(structureUI);
            return structureUI;
        }

        Debug.LogError($"<color=red>({uiType}) is not type of StructureUIBase</color>");
        return null;
    }

    private Type GetUIType(Structure.SpecialStructureType structureType) {
        switch (structureType) {
            case Structure.SpecialStructureType.None:
            default:
                Debug.LogError($"<color=red>structure type null</color>");
                return null;
            case Structure.SpecialStructureType.Radio:
                return typeof(RadioPopUpUI);
        }
    }
}