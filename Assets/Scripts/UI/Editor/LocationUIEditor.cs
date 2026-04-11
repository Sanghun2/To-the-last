using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocationMarkerUI), true)]
public class LocationUIEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (LocationMarkerUI)target;
        if (GUILayout.Button("Save Location Position")) {
            _script.SaveCurrentLocationPosition();
        }
    }
}
