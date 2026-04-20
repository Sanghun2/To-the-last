using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestTester))]
public class QuestTesterEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var _script = (QuestTester)target;
        if (GUILayout.Button("Publish Quest")) {
            _script.PublishTestQuest();
        }
        if (GUILayout.Button("Add Task Count")) {
            _script.AddTaskCount();
        }
        if (GUILayout.Button("Remove Task Count")) {
            _script.RemoveTaskCount();
        }
        if (GUILayout.Button("Complete Task")) {
            _script.CompleteTask();
        }
        if (GUILayout.Button("Add Test Item")) {
            _script.AddTestItem();
        }
        if (GUILayout.Button("Log Item Count")) {
            _script.LogCurrentItem();
        }
    }
}
