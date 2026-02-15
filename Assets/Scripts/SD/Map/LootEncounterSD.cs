using UnityEngine;

[CreateAssetMenu(fileName = "LootEncounterSD", menuName = "Scriptable Objects/Encounter/LootEncounterSD")]
public class LootEncounterSD : EncounterSD
{
    public override IEncounterExecutor CreateExecutor() {
        return new LootEncounterExecutor();
    }
}