using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "StorySD", menuName = "Scriptable Objects/Story/StorySD")]
public class StorySD : SDBase
{
    public IReadOnlyList<DialogSD> Dialogs => dialogs;

    [SerializeField] DialogSD[] dialogs;
}
