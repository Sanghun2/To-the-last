using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StructureSD", menuName = "Scriptable Objects/StructureSD")]
public class StructureSD : IconSDBase
{
    public string UIPrefabPath => uiPrefabPath;
    public IReadOnlyList<ItemSD> RequirementItems => requirementItems;

    public float BuildingTime => buildingTime;

    [SerializeField] string uiPrefabPath;
    [SerializeField] float buildingTime = 100;
    [SerializeField] ItemSD[] requirementItems;


    private void OnValidate() {
        RenameAsset(ID, suffix: "_StructureSD");
    }
}
