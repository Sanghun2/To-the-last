using System.Collections.Generic;
using UnityEngine;

public class ShowConstructionUIAction : ActionBase<IReadOnlyList<StructureSD>>
{
    //public ShowConstructionUIAction(IReadOnlyList<StructureSD> constructionList) {
    //    SetParameter(constructionList);
    //}

    public override void Execute() {
        Managers.UI.OpenUI<ConstructionUI>().ShowConstructionList(parameter);
    }
}
