using System;
using UnityEngine;

public sealed class DialogTester : MonoBehaviour
{
    [SerializeField] DialogEncounterSD testDialog;

    public void StartDialog() {
        Managers.Encounter.ExecuteEncounter(testDialog);
    }
}
