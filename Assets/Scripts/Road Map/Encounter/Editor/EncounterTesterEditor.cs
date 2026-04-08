using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EncounterTester))]
public class EncounterTesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (EncounterTester)target;
        if (GUILayout.Button("Create New Main Location")) {
            _script.UnlockMainLocation();
        }
        if (GUILayout.Button("Create New Sub Location")) {
            _script.UnlockSubLocation();
        }
    }
}
