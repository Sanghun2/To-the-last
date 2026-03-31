using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;


public class ConstructionManager : IInitializable
{
    public bool IsInit => _isInit;
    public int CurrentLocationIndex => currentLocationIndex;

    [SerializeField] List<Structure> structureList = new List<Structure>();
    
    private int currentLocationIndex;
    private StructureDataBase currentStructureData;

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
        currentLocationIndex = locationIndex;
    }
    public void SetTargetStructure(StructureSD structureSD) {
        if (!dataParserContainer.TryGet(structureSD, out var parser)) { Debug.LogError($"<color=red>data parser type of ({structureSD.GetType()}) is not exist. </color>"); return; }
        var structureData = parser.ParseData(structureSD);
        SetTargetStructure(structureData);
    }

    public Structure GetStructure(int locationIndex) {
        if (!IsValidLocation(locationIndex)) { return null; }
        return structureList[locationIndex];
    }

    public void ConstructSetTarget(Action onStart = null, Action<float, float> onProgress = null, Action onComplete = null) {
        if (!CanConstruct()) return;
        StartConstruction(currentLocationIndex, currentStructureData, onStart, onProgress, onComplete);
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
    public void DestroyStructureAt(int locationIndex, Action onDestroyComplete=null) {
        if (!IsValidLocation(locationIndex)) { return; }
        if (IsEmpty(locationIndex)) { return; }

        Structure targetStructure = GetStructure(locationIndex);
        StructureContextBase structureContext = targetStructure.StructureContext;
        //var buildingUI = Managers.UI.GetUI<ConstructionUI>();
        FocusJob destroyJob = new FocusJob(
            structureContext.ConstructionTime,
            onComplete: () => {
                targetStructure.DestroyStrucure();
                Managers.UI.CloseAllUIs();
                onDestroyComplete?.Invoke();
            }).WithBlockScreen();
        Managers.Job.DoFocusJob(destroyJob);
    }
    public void DestroyCurrentStructure() {
        if (currentLocationIndex >= 0) {
            DestroyStructureAt(currentLocationIndex);
        }
    }


    private void SetTargetStructure(StructureDataBase structureData) {
        currentStructureData = structureData;
    }
    private bool CanConstruct() {
        Debug.Log($"index? {currentLocationIndex}, structure? {currentStructureData?.ID ?? "null"}");
        if (currentStructureData == null) { return false; }
        if (!Managers.Inventory.TryGetInventoryByTag(out var inventories, "player", "storage")) { return false; }

        var a = InventoryUtility.HasIngredients(inventories, currentStructureData.RequirementItems);
        return true;
    }
    private void StartConstruction(int locationIndex, StructureDataBase structureData, Action onStart = null, Action<float, float> onProgress = null, Action onComplete = null) {
        if (!IsValidLocation(locationIndex)) return;
        if (!IsEmpty(locationIndex)) return;
        if (!IsValidStructure(structureData)) return;

        CreateConstructionJob(locationIndex, structureData, onStart, onProgress, onComplete);
    }
    private void CreateConstructionJob(int locationIndex, StructureDataBase structureData, Action onStart=null, Action<float, float> onProgress=null, Action onComplete=null) {
        FocusJob constructionJob = new FocusJob(
             structureData.ConstructionTime,
             onStart: onStart,
             onProgress: onProgress,
             onComplete: () => {
                 TryConstructStructure(locationIndex, structureData);
                 onComplete?.Invoke();
             }).WithBlockScreen();


        Managers.Job.DoFocusJob(constructionJob, () => {
            ClearTargetStructure();
            //Managers.UI.CloseUI<ConstructionUI>();
        });
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
        currentStructureData = null;
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
