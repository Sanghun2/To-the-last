using UnityEngine;

[CreateAssetMenu(fileName = "CombatEncounterSD", menuName = "Scriptable Objects/Encounter/CombatEncounterSD")]
public class CombatEncounterSD : EncounterSD
{
    public override IEncounterExecutor CreateExecutor() {
        throw new System.NotImplementedException();
    }
}
