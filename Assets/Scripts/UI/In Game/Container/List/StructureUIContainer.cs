using System.Collections.Generic;
using System.Linq;
using BilliotGames;
using UnityEngine;

public class StructureUIContainer : UIBase
{
    public int Count => structureUIList.Count;

    [SerializeField] List<StructureButton> structureUIList = new List<StructureButton>();


    public override void InitUI() {
        if (IsInit) return;

        structureUIList.Clear();
        structureUIList = GetComponentsInChildren<StructureButton>().ToList();
        for (int i = 0; i < structureUIList.Count; i++) {
            structureUIList[i].AssignIndex(i);
            structureUIList[i].InitUI();
        }

        _isInit = true;
    }

    public StructureButton GetStructureUI(int index) {
        if (0 <= index && index < structureUIList.Count) {
            return structureUIList[index];
        }
        else {
            return null;
        }
    }

    public void Release() {
        structureUIList.Clear();
        _isInit = false;
    }
}