#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocationInfoSD))]
public class LocationInfoSDEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var sd = (LocationInfoSD)target;

        if (GUILayout.Button("Auto Assign Main Image"))
            AssignSprite(sd, 
                $"{sd.ID}", 
                "Assets/@Resources/Image/Map", 
                "image", 
                "MainImage");

        if (GUILayout.Button("Auto Assign Icon"))
            AssignSprite(sd, 
                $"{sd.ID}_Icon", 
                $"{Define.Path.ICON_ASSET_LOAD_PATH}/Location", 
                "iconImage", 
                "Icon");
    }

    static void AssignSprite(LocationInfoSD sd, string searchName, string searchPath, string propertyName, string label) {
        string[] guids = AssetDatabase.FindAssets($"{searchName} t:Sprite", new[] { searchPath });
        if (guids.Length == 0) {
            Debug.LogError($"{searchName} 스프라이트를 찾을 수 없음");
            return;
        }

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

        Undo.RecordObject(sd, $"Auto Assign {label}");
        var so = new SerializedObject(sd);
        so.FindProperty(propertyName).objectReferenceValue = sprite;
        so.ApplyModifiedProperties();

        Debug.Log($"{sd.ID}_{label} 할당 완료: {path}");
    }
}
#endif