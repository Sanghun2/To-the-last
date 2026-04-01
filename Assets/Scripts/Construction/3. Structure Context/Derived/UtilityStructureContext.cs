using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class UtilityStructureContext : StructureContextBase<UtilityStructureData>
{
    public IReadOnlyList<UtilityContentSD> ContentList => Data.ContentList;

    public UtilityStructureContext(UtilityStructureData data) : base(data) {

    }

    public override StructureUIBase OpenStructureUI() {
        var ui = Managers.UI.GetUI<UtilityStructureUI>();
        if (TryGetStructure(out Structure structure)) {
            ui.SetUpUI(structure);
        }
        Managers.UI.OpenUI(ui);
        return ui;
    }
}
