using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogTester))]
public class DialogTesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (DialogTester)target;
        if (GUILayout.Button("Start Dialog")) {
            _script.StartDialog();
        }
    }
}
