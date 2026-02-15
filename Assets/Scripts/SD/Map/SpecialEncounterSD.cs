using UnityEngine;

[CreateAssetMenu(fileName = "SpecialEncounterSD", menuName = "Scriptable Objects/Encounter/SpecialEncounterSD")]
public class SpecialEncounterSD : EncounterSD
{
    public override IEncounterExecutor CreateExecutor() {
        return new SpecialEncounterExecutor();
    }
}
