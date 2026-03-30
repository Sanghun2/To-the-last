using UnityEngine;

public class ProductionStructureContext : StructureContextBase<ProductionStructureData>
{
    public ProductionStructureContext(ProductionStructureData data) : base(data) {
    }

    public override StructureUIBase OpenStructureUI() {
        var structureUI = Managers.UI.OpenUI<CraftStructureUI>();
        structureUI.InitProgressUI(0, 1); // 추후 현재 작업중인 내용의 progress로 변경
        structureUI.ShowList(Data.Prouctions);
        return structureUI;
    }
}
