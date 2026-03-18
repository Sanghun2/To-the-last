using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocationUI), true)]
public class LocationUIEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (LocationUI)target;
        if (GUILayout.Button("Save Location Position")) {
            _script.SaveCurrentLocationPosition();
        }
    }
}
