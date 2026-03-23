using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TraitTester))]
public class TraitTesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (TraitTester)target;
        GUILayout.Space(20);
        if(GUILayout.Button("Show Trait List")) {
            _script.ShowTraitList();
        }
        if (GUILayout.Button("Show Selected Trait List")) {
            _script.ShowSelectTraits();
        }

        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Init Trait Data")) {
                _script.InitTraitData();
            }
            if (GUILayout.Button("Set Point")) {
                _script.SetTraitPoint();
            }
        }
        GUILayout.EndHorizontal();
    }
}
