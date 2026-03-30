using UnityEngine;

public abstract class StructureDataParserBase
{
    public abstract bool TryParseData(StructureSD structureSD, out StructureDataBase structureData);
}

public abstract class StructureDataParserBase<TSD, TData> : StructureDataParserBase
    where TSD : StructureSD
    where TData : StructureDataBase
{
    public override bool TryParseData(StructureSD structureSD, out StructureDataBase structureData) {
        if (structureSD is TSD tsd) {
            var result = TryParseData(tsd, out TData parsedData);
            structureData = parsedData;
            return result;
        }

        Debug.LogError($"<color=red>{structureSD.GetType()} is not type of ({typeof(TSD)})</color>");
        structureData = null;
        return false;
    }

    public abstract bool TryParseData(TSD structureSD, out TData structureData);
}
