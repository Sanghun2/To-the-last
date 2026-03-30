using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class ConstructionManager : IInitializable
{
    public bool IsInit => _isInit;

    [SerializeField] List<Structure> structureList = new List<Structure>();
    
    private int targetLocationIndex;
    private StructureDataBase targetStructureData;

    private StructureUIContainer structureUIContainer;
    private StructureDataParserContainer dataParserContainer = new StructureDataParserContainer();
    private StructureContextBuilderContainer contextBuilderContainer = new StructureContextBuilderContainer();
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
        if (!dataParserContainer.TryGet(structureSD, out var parser)) { Debug.LogError($"<color=red>data parser type of ({structureSD.GetType()}) is not exist. </color>"); return; }
        var structureData = parser.ParseData(structureSD);
        SetTargetStructure(structureData);
    }

    public void ConstructSetTarget() {
        if (!CanConstruct()) return;
        StartConstruction(targetLocationIndex, targetStructureData);
    }

    public void UnlockLocation(int locationIndex) {
        var structure = GetStructure(locationIndex);
        if (structure != null && structure.IsLocked) {
            structure.Unlock();
        }

        else {
            Debug.LogAssertion($"unlock failed. ui null? {structure == null}, state: Empty != {structure.CurrentState}");
        }
    }
    public void DestroyStructure(int locationIndex) {
        if (!IsValidLocation(locationIndex)) { return; }
        if (IsEmpty(locationIndex)) { return; }

        var targetStructure = GetStructure(locationIndex);
        var structureSD = targetStructure.StructureContext;
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


    private void SetTargetStructure(StructureDataBase structureData) {
        targetStructureData = structureData;
    }
    private bool CanConstruct() {
        Debug.Log($"index? {targetLocationIndex}, structure? {targetStructureData?.ID ?? "null"}");
        if (targetStructureData == null) { return false; }
        if (!Managers.Inventory.TryGetInventoryByTag(out var inventories, "player", "storage")) { return false; }

        var a = InventoryUtility.HasIngredients(inventories, targetStructureData.RequirementItems);
        return true;
    }
    private void StartConstruction(int locationIndex, StructureDataBase structureData) {
        if (!IsValidLocation(locationIndex)) return;
        if (!IsEmpty(locationIndex)) return;
        if (!IsValidStructure(structureData)) return;

        CreateConstructionJob(locationIndex, structureData);
    }
    private void CreateConstructionJob(int locationIndex, StructureDataBase structureData) {
        FocusJob constructionJob = new FocusJob(
             structureData.ConstructionTime,
             onProgressChanged: (ctime, mtime) => {
                 Managers.UI.GetUI<ConstructionUI>().UpdateProgressBar(ctime, mtime);
             },
             onComplete: () => TryConstructStructure(locationIndex, structureData));


        Managers.Job.DoFocusJob(constructionJob, () => {
            ClearTargetStructure();
            //Managers.UI.CloseUI<ConstructionUI>();
        });
    }
    private Structure GetStructure(int locationIndex) {
        if (!IsValidLocation(locationIndex)) { return null; }
        return structureList[locationIndex];
    }

    private bool TryConstructStructure(int targetLocationIndex, StructureDataBase targetStructureData) {
        var requireIngredients = targetStructureData.RequirementItems;

        // 재료 소모

        // 건설
        if (!contextBuilderContainer.TryGet(targetStructureData, out var contextBuilder)) { Debug.LogError($"<color=red>context builder type of ({targetStructureData.GetType()}) is not exist</color>"); return false; }
        if (!contextBuilder.TryBuildContext(targetStructureData, out var structureContext)) { Debug.LogError($"<color=red>({targetStructureData.GetType()}) context build failed</color>"); return false; }

        Structure targetStructure = GetStructure(targetLocationIndex);
        targetStructure.ConstructStructure(structureContext);

        return true;
    }
    private void ClearTargetStructure() {
        targetStructureData = null;
    }

    /// <summary>
    /// 올바른 struct data인지 체크
    /// </summary>
    /// <param name="structureData"></param>
    /// <returns></returns>
    private bool IsValidStructure(StructureDataBase structureData) {
        if (structureData != null) {
            return true;
        }
        else {
            Debug.LogAssertion($"not valid structure. id: {structureData?.ID ?? "null"}");
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
