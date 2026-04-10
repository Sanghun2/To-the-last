using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogBookSD", menuName = "Scriptable Objects/Dialog/DialogBookSD")]
public class DialogBookSD : SDBase
{
    public IReadOnlyList<DialogPageSD> Pages => dialogPages;

    [SerializeField] DialogPageSD[] dialogPages;
}
