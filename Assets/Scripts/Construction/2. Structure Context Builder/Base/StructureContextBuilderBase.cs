using UnityEngine;

public abstract class StructureContextBuilderBase
{
    public abstract bool TryBuildContext(StructureDataBase structureData, out StructureContextBase structureContext);
}


public abstract class StructureContextBuilderBase<TData, TContext> : StructureContextBuilderBase
    where TData : StructureDataBase
    where TContext : StructureContextBase
{
    public override bool TryBuildContext(StructureDataBase structureData, out StructureContextBase structureContext) {
        if (structureData is TData data) {
            var result = TryBuildContext(data, out TContext context);
            structureContext = context;
            return result;
        }

        Debug.LogError($"<color=red>({structureData.GetType()}) is not type of ({typeof(TData)})</color>");
        structureContext = null;
        return false;
    }

    public abstract bool TryBuildContext(TData structureData, out TContext structureContext);
}