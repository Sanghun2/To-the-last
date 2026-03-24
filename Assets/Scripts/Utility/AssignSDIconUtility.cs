#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


public class AssignSDIconUtility
{
    [MenuItem("Tools/Auto Assign All Item Icons")]
    private static void AssignAllIcons() {
        string[] guids = AssetDatabase.FindAssets("t:ItemSD");
        int successCount = 0;
        int skipCount = 0;

        foreach (var guid in guids) {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var itemSD = AssetDatabase.LoadAssetAtPath<ItemSD>(path);

            if (itemSD == null) continue;

            var so = new SerializedObject(itemSD);
            var prop = so.FindProperty("image");
            var currentSprite = prop.objectReferenceValue as Sprite;
            string expectedName = $"{itemSD.ID}_Icon";

            // 이미 올바른 스프라이트가 할당돼 있으면 스킵
            if (currentSprite != null && currentSprite.name == expectedName) {
                ++skipCount;
                continue;
            }

            string[] iconGuids = AssetDatabase.FindAssets(
                $"{expectedName} t:Sprite",
                new[] { $"{Define.Path.ICON_ASSET_LOAD_PATH}/Item" }
            );

            if (iconGuids.Length == 0) {
                Debug.LogWarning($"{expectedName} 스프라이트를 찾을 수 없음");
                continue;
            }

            var iconPath = AssetDatabase.GUIDToAssetPath(iconGuids[0]);
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);

            Undo.RecordObject(itemSD, "Auto Assign Item Icons");
            prop.objectReferenceValue = sprite;
            so.ApplyModifiedProperties();

            Debug.Log($"{itemSD.ID} 재할당: {currentSprite?.name ?? "null"} → {sprite.name}");
            ++successCount;
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Auto Assign 완료 - 성공: {successCount}, 스킵: {skipCount}");
    }
}
#endif
