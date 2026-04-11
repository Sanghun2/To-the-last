using UnityEngine;

[CreateAssetMenu(fileName = "QuestRegisterSelectionRunnerSD", menuName = "Scriptable Objects/Selection/Runner/QuestRegisterSelectionRunnerSD")]
public class QuestRegisterSelectionRunnerSD : SelectionRunnerSDBase
{
    public QuestSD TargetQuest => targetQeust;

    [SerializeField] QuestSD targetQeust;
}
