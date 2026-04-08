using UnityEngine;

public abstract class SelectionRunnerDataParserBase
{
    public abstract SelectionRunnerDataBase Parse(SelectionRunnerSDBase sd); 
}

public abstract class SelectionRunnerDataParserBase<TSD, TData> : SelectionRunnerDataParserBase
    where TSD : SelectionRunnerSDBase
    where TData : SelectionRunnerDataBase
{
    public abstract TData Parse(TSD sd);

    public override SelectionRunnerDataBase Parse(SelectionRunnerSDBase sd) {
        if (sd is TSD tsd) {
            return Parse(tsd);
        }

        return null;
    }
}


