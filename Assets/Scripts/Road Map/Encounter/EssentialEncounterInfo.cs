using System;
using UnityEngine;

[Serializable]
public class EssentialEncounterInfo
{
    public int Index => index;
    public EncounterSDBase EncounterSD => encounterSD;

    [SerializeField] int index;
    [SerializeField] EncounterSDBase encounterSD;

    public void SetIndex(int index) {
        this.index = index;
    }
}