using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocationSD))]
public class LocationSDEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Auto Assign Icon SD")) {
            var locationIconSD = (LocationSD)target;
            var targetName = $"{locationIconSD.ID}_LocationIconSD";
            string[] guids = AssetDatabase.FindAssets($"{targetName} t:LocationIconSD", new[] {
                $"Assets/Resources/SD/Location Icon"
            });

            if (guids.Length == 0) {
                Debug.LogError($"{targetName}를 찾을 수 없음");
                return;
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var sd = AssetDatabase.LoadAssetAtPath<LocationIconSD>(path);

            Undo.RecordObject(locationIconSD, "Auto Assign Icon SD");
            var so = new SerializedObject(locationIconSD);
            so.FindProperty("locationIcon").objectReferenceValue = sd;
            so.ApplyModifiedProperties();

            Debug.Log($"{locationIconSD.ID}_LocationIconSD 할당 완료: {path}");
        }
    }
}
