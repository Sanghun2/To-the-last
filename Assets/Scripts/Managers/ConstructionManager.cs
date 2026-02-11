using System;
using System.Collections.Generic;
using UnityEngine;


public class ConstructionManager : IInitializable
{
    public bool IsInit => _isInit;

    private StructureUIContainer structureUIContainer;
    private int targetLocationIndex;
    private StructureSD targetStructureSD;
    private List<Structure> structureList = new List<Structure>();
    private bool _isInit;

    public void Init() {
        if (IsInit) return;

        if (structureUIContainer == null) structureUIContainer = GameObject.FindAnyObjectByType<StructureUIContainer>(FindObjectsInactive.Include);
        structureUIContainer.InitUI();

        structureList.Clear();
        for (int i = 0; i < structureUIContainer.Count; i++) {
            structureList.Add(structureUIContainer.GetStructureUI(i).Structure);
        }

        _isInit = true;
    }
    public void Release() {

    }

    public void SetLocationIndex(int locationIndex) {
        targetLocationIndex = locationIndex;
    }
    public void SetTargetStructure(StructureSD structureSD) {
        targetStructureSD = structureSD;
    }

    public void ConstructTarget() {
        Construct(targetLocationIndex, targetStructureSD);
    }
    public void Construct(int locationIndex, StructureSD structureSD) {
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


        Managers.Job.DoFocusJob(constructionJob, () => { ClearTargetStructure(); });
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

    private bool IsValidStructure(StructureSD targetStructureSD) {
        if (targetStructureSD != null) {
            return true;
        }
        else {
            Debug.LogAssertion($"not valid structure. id: {targetStructureSD.ID}");
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
