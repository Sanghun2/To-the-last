using UnityEngine;

public abstract class SelectionDataParserBase
{
    public abstract SelectionDataBase Parse(SelectionSDBase sd); 
}

public abstract class SelectionDataParserBase<TSD, TData> : SelectionDataParserBase
    where TSD : SelectionSDBase
    where TData : SelectionDataBase
{
    public abstract TData Parse(TSD sd);

    public override SelectionDataBase Parse(SelectionSDBase sd) {
        if (sd is TSD tsd) {
            return Parse(tsd);
        }

        return null;
    }
}


