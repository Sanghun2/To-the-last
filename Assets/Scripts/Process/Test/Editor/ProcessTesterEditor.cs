using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProcessTester))]
public class ProcessTesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (ProcessTester)target;

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("◀Prev Process")) {
                _script.PrevProcess();
            }
            GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("Start Process")) {
                _script.StartProcess();
            }
            GUI.backgroundColor = Color.white;
            if (GUILayout.Button("Next Process▶")) {
                _script.NextProcess();
            }
        }
        GUILayout.EndHorizontal(); 
    }
}
