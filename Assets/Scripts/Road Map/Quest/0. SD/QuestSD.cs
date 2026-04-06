using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSD", menuName = "Scriptable Objects/Quest/QuestSD")]
public class QuestSD : SDBase
{
    public TaskSD[] TaskSDs => taskSDs;

    public Quest.Type Type => type;

    [SerializeField] Quest.Type type;
    [SerializeField] TaskSD[] taskSDs;
}
