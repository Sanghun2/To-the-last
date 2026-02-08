using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;



public class BuildingUI : UIBase
{
    [SerializeField] BuildContentUIContainer buildContentUIContainer;

    public void ShowConstructionList(IReadOnlyList<StructureSD> structureList) {
        buildContentUIContainer.ShowList(structureList);
    }
}
