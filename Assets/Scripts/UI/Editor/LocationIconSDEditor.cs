#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(LocationIconSD))]
public class LocationIconSDEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Auto Assign Icon")) {
            var locationIconSD = (LocationIconSD)target;
            string[] guids = AssetDatabase.FindAssets($"{locationIconSD.ID}_Icon t:Sprite", new[] {
                $"{Define.Path.ICON_ASSET_LOAD_PATH}/Location"
            });

            if (guids.Length == 0) {
                Debug.LogError($"{locationIconSD.ID}_Icon 스프라이트를 찾을 수 없음");
                return;
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

            Undo.RecordObject(locationIconSD, "Auto Assign Icon");
            var so = new SerializedObject(locationIconSD);
            so.FindProperty("image").objectReferenceValue = sprite;
            so.ApplyModifiedProperties();

            Debug.Log($"{locationIconSD.ID}_Icon 할당 완료: {path}");
        }
    }
}
#endif