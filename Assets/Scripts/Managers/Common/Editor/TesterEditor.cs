using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tester))]
public class TesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (Tester)target;
        if (GUILayout.Button("Reigster Task")) {
            _script.RegisterTask();
        }
        if (GUILayout.Button("Do Task")) {
            _script.DoTask();
        }
    }
}
