using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterContentBuilderBase
{
    public abstract IReadOnlyList<EncounterSelectCondition> BuildContent();
}
