using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public readonly struct ConstructionContext
{
    public readonly int Index => index;
    public IReadOnlyList<StructureSD> StructureCatalogs => structureCatalogs;

    private readonly int index;
    private readonly IReadOnlyList<StructureSD> structureCatalogs;

    public ConstructionContext(int index, IReadOnlyList<StructureSD> structureCatalogs) {
        this.index = index;
        this.structureCatalogs = structureCatalogs;
    }
}

public class ShowConstructionUIAction : ActionBase<ConstructionContext>
{
    public ShowConstructionUIAction(ConstructionContext context) {
        SetParameter(context);
    }

    public override void Execute() {
        Managers.Construction.SetLocationIndex(parameter.Index);
        var ui = Managers.UI.OpenUI<ConstructionUI>();
        ui.ShowConstructionCatalogs(parameter.StructureCatalogs);
    }
}
