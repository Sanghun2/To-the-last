using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSD", menuName = "Scriptable Objects/Dialog/DialogSD")]
public class DialogPageSD : ImageSDBase
{
    public string TalkerName => talkerName;
    public IReadOnlyList<SelectionSDContext> Selections => selections == null || selections.Count == 0 ? null : selections;

    [SerializeField] string talkerName;
    [SerializeField] List<SelectionSDContext> selections;
}
