#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

public static class LocationUtility
{
    public static MarkerGridGenerator MarkerGridGenerator
    {
        get
        {
            if (_markerGridGenerator == null) {
                _markerGridGenerator = GameObject.FindAnyObjectByType<MarkerGridGenerator>();
            }

            return _markerGridGenerator;
        }
    }

    public static string basementSDID = "basement";
    private static MarkerGridGenerator _markerGridGenerator;

    public static float CalculateDistance(Vector2 startPos, Vector2 endPos) {
        //Debug.Log($"[Test] distance: {Vector2.Distance(startPos, endPos)}");
        return Vector2.Distance(startPos, endPos);
    }
    public static float CalculateDistance(Location currentLocation, Location destination) {
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

    public static Vector2 GenerateRandomLocationCoordinate() {
        return CompletelyRandomCoordinate();
    }
    public static bool TryGetGridOrRandom(int markerIndex, out (int index, Vector2 point) pointInfo) {
        if (MarkerGridGenerator.TryGetGridOrRandom(markerIndex, out pointInfo)) {
            return true;
        }

        return false;
    }

#if UNITY_EDITOR

    private static Vector2 CompletelyRandomCoordinate() => new Vector2(Random.Range(-350, 350), Random.Range(-550, 420));

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