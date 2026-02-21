using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tester))]
public class TesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (Tester)target;

        GUI.enabled = Application.isPlaying;

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("[  Pointer Test  ]");
            if (GUILayout.Button("Move Pointer")) {
                _script.MovePointer();
            }
            if (GUILayout.Button("Pause Pointer")) {
                _script.PausePointer();
            }
        }
        GUILayout.EndHorizontal();


        GUILayout.Space(20);
        GUILayout.Label("[  Selection Test  ]");
        if (GUILayout.Button("Show Selections")) {
            _script.ShowSelections();
        }

        GUILayout.Space(20);
        GUILayout.Label("[  Location Test  ]");
        if (GUILayout.Button("Execute Encounter")) {
            _script.ExecuteEncounter();
        }

        GUILayout.Space(20);
        GUILayout.Label("[  Location Test  ]");
        if (GUILayout.Button("Activate Location")) {
            _script.ActivateLocation();
        }
        if (GUILayout.Button("Deactivate Location")) {
            _script.DeactivateLocation();
        }
        if (GUILayout.Button("Show Location")) {
            _script.ShowLocationPopUp();
        }

        GUILayout.Space(20);
        GUILayout.Label("[  Stat Test  ]");
        if (GUILayout.Button("Change Stat")) {
            _script.ChangeValue();
        }

        GUILayout.Space(20);
        GUILayout.Label("[  Inventory Test  ]");
        if (GUILayout.Button("Push Item")) {
            _script.PushItem();
        }
        if (GUILayout.Button("Pop Item")) {
            _script.PopItem();
        }
        if (GUILayout.Button("Show Inventory")) {
            _script.ShowInventory();
        }

        GUILayout.Space(20);
        GUILayout.Label("[  Job Handler Test  ]");
        if (GUILayout.Button("Reigster Task")) {
            _script.RegisterTask();
        }
        if (GUILayout.Button("Do Task")) {
            _script.DoTask();
        }

        GUILayout.Space(20);
        GUILayout.Label("[  Build UI Test  ]");
        if (GUILayout.Button("Show Build List")) {
            _script.ShowBuildList();
        }
        if (GUILayout.Button("Unlock Structure")) {
            _script.UnlockStructureUI();
        }
        if (GUILayout.Button("Construct Structure")) {
            _script.SetStructure();
        }
        if (GUILayout.Button("Destroy Structure")) {
            _script.Destroy();
        }

        GUI.enabled = true;
    }
}
