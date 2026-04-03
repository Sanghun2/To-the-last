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

#if TEST    
        CheckExpensionLevelValidation();
#endif
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
    private void CheckExpensionLevelValidation() {
        Dictionary<int, int> validationCheck = new Dictionary<int, int>() {
            {0, 4},
            {1, 1},
            {2, 1},
            {3, 1},
            {4, 1},
            {5, 1},
            {6, 1},
            {7, 1},
            {8, 1},
        };
        for (int i = 0; i < structureUIList.Count; i++) {
            var su = structureUIList[i];

            int expensionLevel = su.Structure.ExpensionLevel;
            if (validationCheck.TryGetValue(expensionLevel, out int count)) {
                validationCheck[expensionLevel] = count - 1;
                if (count - 1 < 0) {
                    Debug.LogError($"<color=red>level ({expensionLevel})이 정해진 개수보다 많음</color>");
                    break;
                }
            }
            else {
                Debug.LogError($"<color=red>({expensionLevel})에 해당하는 structure가 없음</color>");
                break;
            }
        }
    }
}