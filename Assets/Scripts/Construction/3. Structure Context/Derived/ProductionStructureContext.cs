using UnityEngine;

public class ProductionStructureContext : StructureContextBase<ProductionStructureData>
{
    public ProductionStructureContext(ProductionStructureData data) : base(data) {
    }

    public override StructureUIBase OpenStructureUI() {
        CraftStructureUI structureUI = Managers.UI.GetUI<CraftStructureUI>();
        if (structureUI.IsOpened) return structureUI;

        var id = Data.ID;
        if (TryGetStructure(out Structure structure)) {
            structureUI.SetUpUI(structure);
        }
        Managers.UI.OpenUI(structureUI);
        return structureUI;
    }
}
