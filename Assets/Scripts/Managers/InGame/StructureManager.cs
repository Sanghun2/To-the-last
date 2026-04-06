using System;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : IInitializable
{
    public Structure CurrentSelctedStructure => currentSelectedStructure;

    public bool IsInit => _isInit;

    [SerializeField] Structure currentSelectedStructure;
    [SerializeField] List<Structure> structureList = new List<Structure>();
    private Dictionary<string, int> constructCountDict = new Dictionary<string, int>();
    private StructureUIContainer structureUIContainer;
    private bool _isInit;

    public event Action<string, int, int> OnStructureCountChanged;

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

        // post init
        InitStructureCount();

        _isInit = true;
    }

    #region Data

    public void SetStructure(Structure structure) {
        this.currentSelectedStructure = structure;
    }
    public Structure GetStructure(int locationIndex) {
        if (!IsValidLocation(locationIndex)) { return null; }
        return structureList[locationIndex];
    }
    public bool TryGetStructure(string id, out Structure structure) {
        structure = structureList.Find(x => x.StructureContext != null && x.StructureContext.ID.Equals(id));
        return structure != null;
    }

    public void ChangeStuctureCount(StructureDataBase structureData, int deltaCount) {
        ChangeStructureCount(structureData.ID, deltaCount);
    }
    public int GetStructureCount(string structureID) {
        return constructCountDict.TryGetValue(structureID, out var structureCount) ? structureCount : 0;
    }

    private void ChangeStructureCount(string structureID, int deltaCount) {
        if (constructCountDict.TryGetValue(structureID, out var structureCount)) {
            var prevCount = structureCount;
            var newCount = structureCount + deltaCount;
            newCount = Mathf.Max(newCount, 0);

            var delatCount = newCount - prevCount;
            constructCountDict[structureID] = newCount;
            OnStructureCountChanged?.Invoke(structureID, newCount, delatCount);
        }
    }

    #endregion

    #region Location

    public bool IsValidLocation(int targetLocationIndex) {
        if (0 <= targetLocationIndex && targetLocationIndex < structureList.Count) {
            return true;
        }

        Debug.LogAssertion($"not valid location - index: {targetLocationIndex}");
        return false;
    }
    public void UnlockLocation(int locationIndex) {
        var structure = GetStructure(locationIndex);
        if (structure != null && structure.IsLocked) {
            structure.Unlock();
        }

        else {
            Debug.LogAssertion($"unlock failed. ui null? {structure == null}, state: Empty != {structure.CurrentStructureState}");
        }
    }
    public void UnlockLocations(int targetExpensionLevel) {
        for (int i = 0; i < structureUIContainer.Count; i++) {
            var structureUI = structureUIContainer.GetStructureUI(i);
            if (structureUI.Structure.ExpensionLevel == 0) {
                structureUI.Structure.Unlock();
                structureUI.UpdateUI();
            }
        }
    }

    #endregion

    public void Release() {
        structureUIContainer?.Release();
    }

    private void InitStructureCount() {
        constructCountDict.Clear();
        for (int i = 0; i < structureList.Count; i++) {
            var structure = structureList[i];

            string structureID = structure.ID;
            if (string.IsNullOrEmpty(structureID)) continue;

            if (constructCountDict.ContainsKey(structureID)) {
                constructCountDict[structure.ID] += 1;
            }
            else {
                constructCountDict[structure.ID] = 1;
            }
        }
    }

}
