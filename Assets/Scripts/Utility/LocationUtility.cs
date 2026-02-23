#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class LocationUtility
{
    private static string basementSDID = "basement";

    public static float CalculateDistance(Vector2 startPos, Vector2 endPos) {
        Debug.Log($"[Test] distance: {Vector2.Distance(startPos, endPos)}");
        return Vector2.Distance(startPos, endPos);
    }
    public static int ConvertToMinutes(this float distance) {
        var h = (int)(distance / 100);
        var m = (int)(distance % 100);
        Debug.Log($"[Test] expected time: {h}시간 {m}분");
        return h * 60 + m;
    }

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
            float dist = CalculateDistance(basementPos, location.AnchoredPosition);

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