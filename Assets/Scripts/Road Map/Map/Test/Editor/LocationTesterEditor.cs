using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocationTester))]
public class LocationTesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (LocationTester)target;
        if (GUILayout.Button("Log Grid")) {
            _script.LogGridPoints();
        }
        if (GUILayout.Button("Set Position To Target")) {
            _script.SetToTestPosition();
        }
    }
}
