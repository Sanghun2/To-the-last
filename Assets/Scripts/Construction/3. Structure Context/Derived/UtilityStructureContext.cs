using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class UtilityStructureContext : StructureContextBase<UtilityStructureData>
{
    public IReadOnlyList<ActivityContentSD> ContentList => Data.ContentList;

    public UtilityStructureContext(UtilityStructureData data) : base(data) {

    }

    public override StructureUIBase OpenStructureUI() {
        var structureUI = Managers.UI.GetUI<UtilityStructureUI>();
        if (structureUI.IsOpened) return structureUI;

        if (TryGetStructure(out Structure structure)) {
            structureUI.SetUpUI(structure);
        }
        Managers.UI.OpenUI(structureUI);
        return structureUI;
    }
}
