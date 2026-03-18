#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StatSD))]
public class StatSDEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Auto Assign Icon")) {
            var statSD = (StatSD)target;
            var targetStatID = statSD.ID;
            string[] guids = AssetDatabase.FindAssets($"{targetStatID}_Icon t:Sprite", new[] {
                $"{Define.Path.ICON_ASSET_LOAD_PATH}/Stat"
            });

            if (guids.Length == 0) {
                Debug.LogError($"{targetStatID}_Icon 스프라이트를 찾을 수 없음");
                return;
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

            Undo.RecordObject(statSD, "Auto Assign Icon");
            var so = new SerializedObject(statSD);
            so.FindProperty("image").objectReferenceValue = sprite;
            so.ApplyModifiedProperties();

            Debug.Log($"{statSD.ID}_Icon 할당 완료: {path}");
        }
    }
}
#endif