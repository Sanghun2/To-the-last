using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;

public class StructureUIContainer : UIBase
{
    [SerializeField] List<StructureUI> structureUIList = new List<StructureUI>();

    public override void InitUI() {
        if (IsInit) return;

        structureUIList.Clear();
        structureUIList = GetComponentsInChildren<StructureUI>().ToList();

        _isInit = true;
    }

    public StructureUI GetStructureUI(int index) {
        if (0 <= index && index < structureUIList.Count) {
            return structureUIList[index];
        }
        else {
            return null;
        }
    }
}
