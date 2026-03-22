using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterTester))]
public sealed class ChracterTesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (CharacterTester)target;
        GUILayout.Space(20);
        if (GUILayout.Button("Show Test Character")) {
            _script.ShowCharacter();
        }
        if (GUILayout.Button("Show Test Character List")) {
            _script.ShowCharacterList();
        }
    }

}
