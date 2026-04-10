using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterMapBuilderBase
{
    public abstract IReadOnlyList<EncounterDataBase> BuildMap(EncounterMapContextBase mapContext);
}

public abstract class EncounterMapBuilderBase<TMapContext> : EncounterMapBuilderBase
    where TMapContext : EncounterMapContextBase
{
    public override IReadOnlyList<EncounterDataBase> BuildMap(EncounterMapContextBase mapContext) {
        if (mapContext is TMapContext context) {
            return BuildMap(context);
        }

        Debug.LogError($"<color=red>map context is not type of ({typeof(TMapContext)})</color>");
        return null;
    }

    public abstract IReadOnlyList<EncounterDataBase> BuildMap(TMapContext mapContext);
}
