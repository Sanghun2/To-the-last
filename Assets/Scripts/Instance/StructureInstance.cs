using UnityEngine;
using UnityEngine.UI;

public class StructureInstance : MonoBehaviour
{
    [SerializeField] Structure structure;
    [SerializeField] Image structureImage;
    public void InitStructure(StructureSD structureSD) {
        structureImage.sprite = structureSD.IconImage;
        structure = new Structure(structureSD);
    }
    public void SetStructure(Structure structure) {
        this.structure = structure;
    }

    protected virtual void Reset() {
        if (structureImage == null) {
            structureImage = GetComponentInChildren<Image>();
        }
    }
}
