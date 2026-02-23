using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class SelectionContext
{

}

public abstract class SelectionHandler
{
    public abstract void Execute(SelectionSD selectionSD, SelectionContext context);
}
public abstract class SelectionHandler<TSelection, TSelectionContext> : SelectionHandler
    where TSelection : SelectionSD
    where TSelectionContext : SelectionContext
{
    public override void Execute(SelectionSD selectionSD, SelectionContext context) {
        var convertedContext = context as TSelectionContext;
        Execute((TSelection)selectionSD, convertedContext);
    }
    public abstract void Execute(TSelection selectionSD, TSelectionContext context = null);
}

public class SelectionSystem
{
    private Dictionary<Type, SelectionHandler> selectionHandlerDict = new Dictionary<Type, SelectionHandler>();    

    public SelectionSystem() {
        RegisterHandler(new LootSelectionHandler());
    }

    public void ExecuteSelection(SelectionSD selectionSD, SelectionContext context=null) {
        if (TryGetHandler(selectionSD, out var handler)) {
            handler.Execute(selectionSD, context);
            Debug.Log($"{selectionSD.GetType().Name} selection 실행");
        }
    }

    private void RegisterHandler<TSelection, TSelectionContext>(SelectionHandler<TSelection, TSelectionContext> handler) 
        where TSelection : SelectionSD 
        where TSelectionContext : SelectionContext
        {
        if (selectionHandlerDict.TryAdd(typeof(TSelection), handler) == false) {
            Debug.LogError($"{typeof(TSelection)} selection handler 중복");
        }
    }
    private void UnregisterHandler<TSelection>() where TSelection : SelectionSD {
        selectionHandlerDict.Remove(typeof(TSelection));
    }

    private bool TryGetHandler(SelectionSD selectionSD, out SelectionHandler handler) {
        if (selectionHandlerDict.TryGetValue(selectionSD.GetType(), out handler)) {
            return true;
        }

        Debug.LogError($"({selectionSD.GetType()}) selection handler not found");
        return false;
    }
}
