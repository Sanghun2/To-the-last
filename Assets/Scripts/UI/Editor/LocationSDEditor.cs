#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(LocationSD))]
public class LocationSDEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Auto Assign Icon")) {
            var locationSD = (LocationSD)target;
            string[] guids = AssetDatabase.FindAssets($"{locationSD.ID}_Icon t:Sprite", new[] {
                $"{Define.Path.ICON_ASSET_LOAD_PATH}/Location"
            });

            if (guids.Length == 0) {
                Debug.LogError($"{locationSD.ID}_Icon 스프라이트를 찾을 수 없음");
                return;
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

            Undo.RecordObject(locationSD, "Auto Assign Icon");
            var so = new SerializedObject(locationSD);
            so.FindProperty("iconImage").objectReferenceValue = sprite;
            so.ApplyModifiedProperties();

            Debug.Log($"{locationSD.ID}_Icon 할당 완료: {path}");
        }
    }
}
#endif