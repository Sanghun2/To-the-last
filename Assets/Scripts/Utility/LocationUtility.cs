using System;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class LocationUtility
{
    public static string basementSDID = "basement";

    public static float CalculateDistance(Vector2 startPos, Vector2 endPos) {
        //Debug.Log($"[Test] distance: {Vector2.Distance(startPos, endPos)}");
        return Vector2.Distance(startPos, endPos);
    }
    public static float CalculateDistance(LocationSD currentLocation, LocationSD destination) {
        return CalculateDistance(currentLocation.AnchoredPosition, destination.AnchoredPosition);
    }
    public static int ConvertToMinutes(this float distance) {
        var h = GetHour(distance);
        var m = GetMinute(distance);
        Debug.Log($"[Test] expected time: {h}시간 {m}분");
        return h * 60 + m;
    }
    public static (int hour, int minutes) ConvertToTime(this float distance) {
        return (GetHour(distance), GetMinute(distance));
    }
 
    public static Location FindLocation(string locationID) {
        if (Managers.Location.TryGetLocation(locationID, out Location location)) {
            return location;
        }

        return null;
    }


#if UNITY_EDITOR

    [MenuItem("Tools/Location/Recalculate Location Distances")]
    public static void Recalculate() {
        // 기준 집 RunnerSD 찾기
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
    private static LocationSD FindHomeSD() {
        var guids = AssetDatabase.FindAssets("t:LocationSD");

        foreach (var guid in guids) {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var sd = AssetDatabase.LoadAssetAtPath<LocationSD>(path);

            if (sd.ID.Equals(basementSDID)) // 기준 조건
                return sd;
        }
        return null;
    }
#endif

    private static int GetHour(float distance) {
        distance /= 2;
        return (int)(distance / 60);
    }
    private static int GetMinute(float distance) {
        distance /= 2;
        return (int)(distance % 60);
    }

}