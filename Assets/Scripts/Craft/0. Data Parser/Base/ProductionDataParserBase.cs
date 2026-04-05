using System;
using UnityEngine;

public abstract class ProductionDataParserBase
{
    public abstract ProductionDataBase ParseData(ContentSDBase targetContent);
}

public abstract class ProductionDataParserBase<TSD, TData> : ProductionDataParserBase
    where TSD : ProductionContentSD
    where TData : ProductionDataBase
{
    public override ProductionDataBase ParseData(ContentSDBase contentSD) {
        if (contentSD is TSD tsd) {
            return ParseData(tsd);
        }

        Debug.LogError($"<color=red>({contentSD.GetType()}) failed to parse ({typeof(TData)})</color>");
        return null;
    }

    public abstract TData ParseData(TSD contentSD);
}
