using UnityEngine;

public abstract class ActionContext
{

}

public abstract class ActionProcessor
{
    public abstract bool TryGenerateAction(ActionContext context, out ActionData actionData);
}
