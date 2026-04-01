using System.Collections.Generic;
using UnityEngine;

public class StructureManager : IInitializable
{
    public Structure CurrentSelctedStructure => currentSelectedStructure;

    public bool IsInit => _isInit;

    [SerializeField] Structure currentSelectedStructure;
    [SerializeField] List<Structure> structureList = new List<Structure>();
    private StructureUIContainer structureUIContainer;
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
    public bool IsValidLocation(int targetLocationIndex) {
        if (0 <= targetLocationIndex && targetLocationIndex < structureList.Count) {
            return true;
        }

        Debug.LogAssertion($"not valid location - index: {targetLocationIndex}");
        return false;
    }

    public void Release() {
        structureUIContainer?.Release();
    }
}
