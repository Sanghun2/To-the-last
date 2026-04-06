using UnityEngine;

public abstract class StructureDataParserBase
{
    public abstract StructureDataBase ParseData(StructureSDBase structureSD);
}

public abstract class StructureDataParserBase<TSD, TData> : StructureDataParserBase
    where TSD : StructureSDBase
    where TData : StructureDataBase
{
    public override StructureDataBase ParseData(StructureSDBase structureSD) {
        if (structureSD is TSD tsd) {
            return ParseData(tsd);
        }

        Debug.LogError($"<color=red>{structureSD.GetType()} is not type of ({typeof(TSD)})</color>");
        return null;
    }

    public abstract TData ParseData(TSD structureSD);
}
