using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductionStructureSD", menuName = "Scriptable Objects/Structure/ProductionStructureSD")]
public class ProductionStructureSD : StructureSDBase, IContentContext<ProductionContentSD>
{
    public IReadOnlyList<ProductionContentSD> ContentList => productions;

    [SerializeField] protected ProductionContentSD[] productions;
}
