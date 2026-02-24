using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionCreator 
{
    public ActionCreator() {
        Reigster<LootSelectionActionContext>(new LootSelectionActionFactory());
    }


    private Dictionary<Type, ActionFactory> actionDict = new();

    public ActionData CreateActionData(ActionContext context) {
        if (TryGetFactory(context.GetType(), out var factory)) {
            return factory.CreateAction(context);
        }

        return null;
    }

    private bool TryGetFactory(Type type, out ActionFactory actionFactory) {
        if (actionDict.TryGetValue(type, out actionFactory)) {
            return true;
        }

        Debug.LogError($"<color=red>couldn't find ({type}) action factory. register first</color>");
        return false;
    }

    public void Reigster<TActionContext>(ActionFactory factory) where TActionContext : ActionContext {
        if (!actionDict.TryAdd(typeof(TActionContext), factory)) {
            Debug.LogError($"<color=red>{typeof(TActionContext)} 중복</color>");
        }
    }
    public void Unregister<TActionFactory>() where TActionFactory : ActionFactory {
        actionDict.Remove(typeof(TActionFactory));
    }
}
