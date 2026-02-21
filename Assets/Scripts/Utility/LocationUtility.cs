#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;

public static class LocationUtility
{
    private static string basementSDID = "basement";

    [MenuItem("Tools/Location/Recalculate Location Distances")]
    public static void Recalculate() {
        // 기준 집 SD 찾기
        LocationSD basementSD = FindHomeSD();
        if (basementSD == null) {
            Debug.LogError("basementSD not found");
            return;
        }

        Vector2 basementPos = basementSD.AnchoredPosition;

        // 모든 Location 로드
        var guids = AssetDatabase.FindAssets("t:LocationSD");

        foreach (var guid in guids) {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var location = AssetDatabase.LoadAssetAtPath<LocationSD>(path);

            if (location.ID.Equals(basementSDID)) continue;
            float dist = Vector2.Distance(basementPos, location.AnchoredPosition);

            Undo.RecordObject(location, "Recalculate Distance");
            location.SetDistance(dist);
            EditorUtility.SetDirty(location);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Location distances recalculated");
    }

    static LocationSD FindHomeSD() {
        var guids = AssetDatabase.FindAssets("t:LocationSD");

        foreach (var guid in guids) {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var sd = AssetDatabase.LoadAssetAtPath<LocationSD>(path);

            if (sd.ID.Equals(basementSDID)) // 기준 조건
                return sd;
        }
        return null;
    }
}
#endif