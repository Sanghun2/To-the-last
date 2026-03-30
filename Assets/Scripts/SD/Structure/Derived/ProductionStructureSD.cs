using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductionStructureSD", menuName = "Scriptable Objects/Structure/ProductionStructureSD")]
public class ProductionStructureSD : StructureSD, IContentContext<RecipeSD>
{
    public IReadOnlyList<RecipeSD> ContentList => productions;

    [SerializeField] protected RecipeSD[] productions;
}
