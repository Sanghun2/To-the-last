using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSD", menuName = "Scriptable Objects/DialogSD")]
public class DialogSD : SDBase
{
    [SerializeField] DialogSD entryDialog;
    [SerializeField] DialogSD unlockDialog;
}
