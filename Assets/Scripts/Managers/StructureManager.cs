using System.Collections.Generic;
using UnityEngine;

public sealed class StructureManager
{
    private Dictionary<int, Structure> structureDict = new Dictionary<int, Structure>();

    public bool TryAddStructure(int spotIndex, Structure structure) {
        if (structureDict.TryAdd(spotIndex, structure)) {
            return true;
        }

        Debug.LogError($"({spotIndex})에 구조물을 설치할 수 없음");
        return false;
    }

    public void RemoveStructure(int spotIndex) {
        structureDict.Remove(spotIndex);
    }


}
