using UnityEngine;

public abstract class ActionConverter
{
    public abstract bool TryConvertAction(ActionContextBase context, out ActionData actionData);
}
