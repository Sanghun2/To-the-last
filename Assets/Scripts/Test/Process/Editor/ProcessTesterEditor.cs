using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProcessTester))]
public class ProcessTesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GUILayout.Space(20);
        if (GUILayout.Button("")) {

        }
    }
}
