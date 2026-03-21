using System.Collections.Generic;
using UnityEngine;

public interface IApplyContext
{
    Entity Caster { get; }
    IReadOnlyList<Entity> Targets { get; }
}
