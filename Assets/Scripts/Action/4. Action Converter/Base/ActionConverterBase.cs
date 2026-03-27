using UnityEngine;

public abstract class ActionConverterBase
{
    public abstract bool TryConvertAction(ActionContextBase context, out ActionData actionData);
}
