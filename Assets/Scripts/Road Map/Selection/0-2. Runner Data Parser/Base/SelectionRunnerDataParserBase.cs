using System;
using UnityEngine;

public abstract class SelectionRunnerDataParserBase
{
    public abstract SelectionRunnerDataBase ParseRunnerData(SelectionRunnerSDBase selectionRunnerSD, int requireMinutes);
}

public abstract class SelectionRunnerDataParserBase<TSD, TData> : SelectionRunnerDataParserBase
    where TSD : SelectionRunnerSDBase
    where TData : SelectionRunnerDataBase
{
    public override SelectionRunnerDataBase ParseRunnerData(SelectionRunnerSDBase selectionRunnerSD, int requireMinutes) {
        if (selectionRunnerSD is TSD tsd) {
            return ParseRunnerData(tsd, requireMinutes);
        }

        Debug.LogError($"<color=red>runnerSD is not type of ({typeof(TSD)})</color>");
        return null;
    }
    public abstract TData ParseRunnerData(TSD tsd, int requireMinutes);
}
