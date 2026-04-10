using UnityEngine;

[CreateAssetMenu(fileName = "DialogEncounterSD", menuName = "Scriptable Objects/Encounter/DialogEncounterSD")]
public class DialogEncounterSD : EncounterSDBase
{
    public DialogBookSD Dialog => dialog;

    [SerializeField] DialogBookSD dialog;
}
