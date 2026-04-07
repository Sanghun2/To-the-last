using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ToastTester))]
public class ToastTesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (ToastTester)target;
        if (GUILayout.Button("Set Presenter")) {
            _script.SetPresenter();
        }

        if (GUILayout.Button("Show Toast")) {
            _script.ShowToast();
        }
    }
}
