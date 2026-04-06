using UnityEditor;
using UnityEngine;

public static class PrefabContextMenu
{
    [MenuItem("GameObject/UI (Canvas)/Default Text", false, 0)]
    static void CreateEnemy(MenuCommand cmd) 
        => Create("Assets/Prefabs/UI/Common/Default Text.prefab", cmd);


    // 유효성 검사 (선택 조건 추가 가능)
    [MenuItem("GameObject/UI (Canvas)/Default Text", true)]
    static bool ValidateCreateMyPrefab() {
        return true; // 조건부로 비활성화하려면 false 반환
    }


    static void Create(string path, MenuCommand cmd) {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null) { Debug.LogError($"Not found: {path}"); return; }

        var go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        GameObjectUtility.SetParentAndAlign(go, cmd.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}
