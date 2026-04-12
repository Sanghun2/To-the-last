using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPCTester))]
public class NPCTesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (NPCTester)target;
        if (GUILayout.Button("Active NPC")) {
            _script.Test_ActiveNPC();
        }
    }
}
