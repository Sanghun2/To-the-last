using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryTester))]
public class InventoryTestEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (InventoryTester)target;
        if (GUILayout.Button("Create Random Items")) {
            _script.CreateRandomItems();
        }

        if (GUILayout.Button("Collect All Items")) {
            _script.CollectAllItems();
        }
    }
}
