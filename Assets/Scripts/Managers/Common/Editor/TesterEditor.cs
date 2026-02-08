using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tester))]
public class TesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (Tester)target;

        GUI.enabled = Application.isPlaying;
        GUILayout.Label("[  Job Handler Test  ]");
        if (GUILayout.Button("Reigster Task")) {
            _script.RegisterTask();
        }
        if (GUILayout.Button("Do Task")) {
            _script.DoTask();
        }

        GUILayout.Space(20);
        GUILayout.Label("[  Build UI Test  ]");
        if (GUILayout.Button("Show Build List")) {
            _script.ShowBuildList();
        }
        if (GUILayout.Button("Set structure to location")) {
            _script.SetStructure();
        }
        GUI.enabled = true;
    }
}
