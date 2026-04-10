using UnityEngine;

public abstract class EncounterContextBuilderBase<TData, TContext> : IEncounterContextBuilder
    where TData : EncounterDataBase
    where TContext : EncounterContextBase
{
    public abstract TContext BuildContext(TData data);

    public EncounterContextBase BuildEncounterContext(EncounterDataBase data) {
        if (data is TData convertedData) {
            return BuildContext(convertedData);
        }

        data = null;
        return null;
    }
}