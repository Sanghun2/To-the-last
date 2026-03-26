using UnityEngine;

public abstract class SelectionDataParserBase
{
    public abstract SelectionDataBase Parse(SelectionSD sd); 
}

public abstract class SelectionDataParserBase<TSD, TData> : SelectionDataParserBase
    where TSD : SelectionSD
    where TData : SelectionDataBase
{
    public abstract TData Parse(TSD sd);

    public override SelectionDataBase Parse(SelectionSD sd) {
        if (sd is TSD tsd) {
            return Parse(tsd);
        }

        return null;
    }
}


