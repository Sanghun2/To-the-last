using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductionStructureSD", menuName = "Scriptable Objects/Structure/ProductionStructureSD")]
public class ProductionStructureSD : StructureSD
{
    public IReadOnlyList<RecipeSD> Prouctions => productions;

    [SerializeField] protected RecipeSD[] productions;

    public override Type GetUIType() {
        return typeof(CraftStructureUI);
    }
}
