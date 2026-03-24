#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ItemSD))]
public class ItemSDEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Auto Assign Icon")) {
            var itemSD = (ItemSD)target;
            string[] guids = AssetDatabase.FindAssets($"{itemSD.ID}_Icon t:Sprite", new[] {
                $"{Define.Path.ICON_ASSET_LOAD_PATH}/Item"
            });

            if (guids.Length == 0) {
                Debug.LogError($"{itemSD.ID}_Icon 스프라이트를 찾을 수 없음");
                return;
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

            Undo.RecordObject(itemSD, "Auto Assign Icon");
            var so = new SerializedObject(itemSD);
            so.FindProperty("image").objectReferenceValue = sprite;
            so.ApplyModifiedProperties();

            Debug.Log($"{itemSD.ID}_Icon 할당 완료: {path}");
        }
    }
}
#endif
