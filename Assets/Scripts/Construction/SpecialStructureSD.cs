using UnityEngine;

[CreateAssetMenu(fileName = "SpecialStructureSD", menuName = "Scriptable Objects/Structure/SpecialStructureSD")]
public class SpecialStructureSD : StructureSDBase
{
    public Structure.SpecialStructureType StructureType => structureType;

    [SerializeField] Structure.SpecialStructureType structureType;
}
