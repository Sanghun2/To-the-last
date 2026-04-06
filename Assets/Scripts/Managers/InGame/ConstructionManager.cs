using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;


public class ConstructionManager : IInitializable
{
    public bool IsInit => _isInit;
    public int CurrentLocationIndex => currentLocationIndex;
    public StructureDataParserContainer StructureDataParserContainer => dataParserContainer;
    public StructureContextBuilderContainer StructureContextBuilderContainer => contextBuilderContainer;

    
    private int currentLocationIndex;
    private StructureDataBase currentStructureData;

    private StructureDataParserContainer dataParserContainer = new StructureDataParserContainer();
    private StructureContextBuilderContainer contextBuilderContainer = new StructureContextBuilderContainer();
    private bool _isInit;

    public void Init() {
        if (IsInit) return;

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

    
    public void ConstructCurrentTarget(Action onStart = null, Action<float, float> onProgress = null, Action onComplete = null) {
        if (!CanConstruct()) return;
        StartConstruction(currentLocationIndex, currentStructureData, onStart, onProgress, onComplete);
    }


    public void DestroyCurrentStructure() {
        if (currentLocationIndex >= 0) {
            DestroyStructureAt(currentLocationIndex);
        }
    }
    public void DestroyStructureAt(int locationIndex, Action onDestroyComplete=null) {
        if (!Managers.Structure.IsValidLocation(locationIndex)) { return; }
        if (IsEmpty(locationIndex)) { return; }

        Structure targetStructure = Managers.Structure.GetStructure(locationIndex);
        StructureContextBase structureContext = targetStructure.StructureContext;

        FocusJob destroyJob = new FocusJob(
            structureContext.ConstructionTime,
            onComplete: () => {
                targetStructure.DestroyStrucure();
                Managers.Structure.ChangeStuctureCount(structureContext.Data, -1);
                Managers.UI.CloseAllUIs();
                onDestroyComplete?.Invoke();
            }).WithBlockScreen();
        Managers.Job.DoFocusJob(destroyJob);
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
        if (!Managers.Structure.IsValidLocation(locationIndex)) return;
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
                 if (TryConstructStructure(locationIndex, structureData)) {
                     Managers.Structure.ChangeStuctureCount(structureData, 1);   
                 }
                 onComplete?.Invoke();
             }).WithBlockScreen();
        Debug.Log($"construction job");

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

        Structure targetStructure = Managers.Structure.GetStructure(targetLocationIndex);
        targetStructure.SetStructure(structureContext);

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
    private bool IsEmpty(int locationIndex) {
        var structure = Managers.Structure.GetStructure(locationIndex);
        if (structure.CurrentStructureState == Structure.StructureState.Empty) {
            return true;
        }

        return false;
    }
}
