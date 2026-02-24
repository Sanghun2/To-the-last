using UnityEngine;

public abstract class ActionContext
{

}

public abstract class ActionFactory
{
    public abstract ActionData CreateAction(ActionContext context);
}
