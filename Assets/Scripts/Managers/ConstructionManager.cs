using System;
using System.Collections.Generic;
using UnityEngine;


public class ConstructionManager : IInitializable
{
    public bool IsInit => _isInit;

    private StructureUIContainer structureUIContainer;
    private int targetLocationIndex;
    private StructureSD targetStructureSD;
    private bool _isInit;

    public void SetLocationIndex(int locationIndex) {
        targetLocationIndex = locationIndex;
    }
    public void SetTarget(StructureSD structureSD) {
        targetStructureSD = structureSD;
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


        Managers.Job.DoFocusJob(constructionJob, () => { ClearTarget(); });
    }
    public void Construct() {
        Construct(targetLocationIndex, targetStructureSD);
    }

    public void Destroy(int locationIndex) {
        if (!IsValidLocation(locationIndex)) { return; }
        if (IsEmpty(locationIndex)) { return; }

        var targetStructureUI = structureUIContainer.GetStructureUI(locationIndex);
        var structureSD = targetStructureUI.StructureSD;
        var buildingUI = Managers.UI.GetUI<ConstructionUI>();
        FocusJob destroyJob = new FocusJob(
            structureSD.ConstructionTime,
            onProgressChanged: (cv, mv) => {
                buildingUI.UpdateProgressBar(cv, mv);
            },
            onComplete: () => {
                targetStructureUI.ClearStructure();
                Managers.UI.CloseUI(buildingUI);
            });
        Managers.Job.DoFocusJob(destroyJob);
    }


    public void Init() {
        if (IsInit) return;

        if (structureUIContainer == null) structureUIContainer = GameObject.FindAnyObjectByType<StructureUIContainer>(FindObjectsInactive.Include);    
        structureUIContainer.InitUI();

        _isInit = true;
    }



    private void ConstructStructure(int targetLocationIndex, StructureSD targetStructureSD) {
        var requireIngredients = targetStructureSD.RequirementItems;

        // 재료 소모

        // 건설
        var targetUI = structureUIContainer.GetStructureUI(targetLocationIndex);
        targetUI.InitStructure(StructureUI.State.Built, targetStructureSD);
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
        // 실제 있고, 설치된 건물 없는지 확인
        var targetUI = structureUIContainer.GetStructureUI(targetLocationIndex);
        if (targetUI != null) {
            return true;
        }

        Debug.LogAssertion($"not valid location - index: {targetLocationIndex}, null?{targetUI == null}, state?{targetUI.CurrentStructureState}");
        return false;
    }
    private bool IsEmpty(int locationIndex) {
        var targetUI = structureUIContainer.GetStructureUI(locationIndex);
        if (targetUI != null && 
            targetUI.CurrentStructureState == StructureUI.State.Empty) {
            return true;
        }

        Debug.Assert(targetUI != null, $"target UI shoudn't be null");
        return false;
    }

    public void Release() {

    }
    private void ClearTarget() {
        targetStructureSD = null;
    }

    internal void Unlock(int locationIndex) {
        var targetUI = structureUIContainer.GetStructureUI(locationIndex);
        if (targetUI != null && targetUI.IsLocked) {
            targetUI.UnlockUI();
        }

        else {
            Debug.LogAssertion($"unlock failed. ui null? {targetUI == null}, state: {targetUI.CurrentStructureState}");
        }
    }
}
