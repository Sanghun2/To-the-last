using System;
using System.Collections.Generic;
using UnityEngine;


public class ConstructionManager : IInitializable
{
    public bool IsInit => _isInit;

    [SerializeField] List<Structure> structureList = new List<Structure>();
    
    private StructureUIContainer structureUIContainer;
    private int targetLocationIndex;
    private StructureSD targetStructureSD;
    private bool _isInit;

    public void Init() {
        if (IsInit) return;

        if (structureUIContainer == null) {
            structureUIContainer = GameObject.FindAnyObjectByType<StructureUIContainer>(FindObjectsInactive.Include);
            if (structureUIContainer == null) {
                Debug.LogError($"<color=red>structure container not found</color>");
                return;
            }
        }


        structureUIContainer?.InitUI();

        structureList.Clear();
        for (int i = 0; i < structureUIContainer.Count; i++) {
            structureList.Add(structureUIContainer.GetStructureUI(i).Structure);
        }

        _isInit = true;
    }
    public void Release() {
        _isInit = false;
    }

    public void SetLocationIndex(int locationIndex) {
        targetLocationIndex = locationIndex;
    }
    public void SetTargetStructure(StructureSD structureSD) {
        targetStructureSD = structureSD;
    }

    public void ConstructTarget() {
        if (!CanConstruct()) return;
        StartConstruction(targetLocationIndex, targetStructureSD);
    }

    private bool CanConstruct() {
        Debug.Log($"index? {targetLocationIndex}, structure? {targetStructureSD.ID}");
        if (targetStructureSD == null) { return false; }
        if (!Managers.Inventory.TryGetInventoryByTag(out var inventories, "player", "storage")) { return false; }

        return InventoryUtility.HasIngredients(inventories, targetStructureSD.RequirementItems);
    }

    private void StartConstruction(int locationIndex, StructureSD structureSD) {
        if (!IsValidLocation(locationIndex)) return;
        if (!IsEmpty(locationIndex)) return;
        if (!IsValidStructure(structureSD)) return;

        FocusJob constructionJob = new FocusJob(
            structureSD.ConstructionTime,
            onProgressChanged: (cv, mv) => {
                Managers.UI.GetUI<ConstructionUI>().UpdateProgressBar(cv, mv);
            },
            onComplete: () => {
                ConstructStructure(locationIndex, structureSD);
            });


        Managers.Job.DoFocusJob(constructionJob, () => {
            ClearTargetStructure();
            //Managers.UI.CloseUI<ConstructionUI>();
        });
    }

    public void Unlock(int locationIndex) {
        var structure = GetStructure(locationIndex);
        if (structure != null && structure.IsLocked) {
            structure.Unlock();
        }

        else {
            Debug.LogAssertion($"unlock failed. ui null? {structure == null}, state: Empty != {structure.CurrentState}");
        }
    }
    public void Destroy(int locationIndex) {
        if (!IsValidLocation(locationIndex)) { return; }
        if (IsEmpty(locationIndex)) { return; }

        var targetStructure = GetStructure(locationIndex);
        var structureSD = targetStructure.StructureSD;
        var buildingUI = Managers.UI.GetUI<ConstructionUI>();
        FocusJob destroyJob = new FocusJob(
            structureSD.ConstructionTime,
            onProgressChanged: (cv, mv) => {
                buildingUI.UpdateProgressBar(cv, mv);
            },
            onComplete: () => {
                targetStructure.DestroyStrucure();
                Managers.UI.CloseUI(buildingUI);
            });
        Managers.Job.DoFocusJob(destroyJob);
    }


    private Structure GetStructure(int locationIndex) {
        if (!IsValidLocation(locationIndex)) { return null; }
        return structureList[locationIndex];
    }

    private void ConstructStructure(int targetLocationIndex, StructureSD targetStructureSD) {
        var requireIngredients = targetStructureSD.RequirementItems;

        // 재료 소모

        // 건설
        var targetStructure = GetStructure(targetLocationIndex);
        targetStructure.ConstructStructure(targetStructureSD);
    }
    private void ClearTargetStructure() {
        targetStructureSD = null;
    }

    /// <summary>
    /// 올바른 struct data인지 체크
    /// </summary>
    /// <param name="targetStructureSD"></param>
    /// <returns></returns>
    private bool IsValidStructure(StructureSD targetStructureSD) {
        if (targetStructureSD != null) {
            return true;
        }
        else {
            Debug.LogAssertion($"not valid structure. id: {targetStructureSD?.ID ?? "null"}");
            return false;
        }
    }
    private bool IsValidLocation(int targetLocationIndex) {
        if (0 <= targetLocationIndex && targetLocationIndex < structureList.Count) {
            return true;
        }

        Debug.LogAssertion($"not valid location - index: {targetLocationIndex}");
        return false;
    }
    private bool IsEmpty(int locationIndex) {
        var structure = GetStructure(locationIndex);
        if (structure.CurrentState == Structure.StructureState.Empty) {
            return true;
        }

        return false;
    }
}
