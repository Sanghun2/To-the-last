using System;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : IInitializable
{
    public bool IsInit => _isInit;

    private Dictionary<Type, IEncounterExecutor> executorDict = new();
    private Dictionary<Type, IEncounterContextFactory> factoryDict = new();
    private bool _isInit;



    public void Init() {
        if (IsInit) return;

        RegisterBundle(new CombatEncounterBundle());
        RegisterBundle(new LootEncounterBundle());
        RegisterBundle(new SpecialEncounterBundle());

        _isInit = true;
    }
    public void Release() {

    }

    public void ExecuteEncounter(EncounterSD encounterSD) {
        var sdType = encounterSD.GetType();
        Debug.Log($"context type: {sdType}");
        if (!TryGetContextFactory(sdType, out var factory)) {
            Debug.LogError($"<color=red>{sdType}에 해당하는 context factory 없음</color>");
            return;
        }

        var context = factory.CreateContext(encounterSD);
        if (context == null) {
            Debug.LogError($"<color=red>생성된 context null</color>");
            return;
        }

        var contextType = context.GetType();
        Debug.Log($"context type: {contextType}");
        if (!TryGetExecutor(contextType, out var executor)) {
            Debug.LogError($"<color=red>{contextType}에 해당하는 executor 없음</color>");
        }

        executor.ExecuteEncounter(context);
    }

    public void ExecuteEncounter<TSD>(EncounterContext<TSD> context)
        where TSD: EncounterSD 
    {
        if (TryGetExecutor(context.GetType(), out IEncounterExecutor executor)) {
            executor.ExecuteEncounter(context);
        }
    }


    private void RegisterBundle<TContext, TSD>(EncounterBundle<TContext, TSD> bundle)
    where TContext : EncounterContext<TSD>
    where TSD : EncounterSD {
        RegisterExecutor(bundle.Executor);
        RegisterContextFactory(bundle.Factory);
    }

    private void RegisterExecutor<TContext, TSD>(EncounterExecutorBase<TContext, TSD> executor)
        where TContext : EncounterContext<TSD>
        where TSD : EncounterSD {
        executorDict[typeof(TContext)] = executor;
    }

    private void RegisterContextFactory(IEncounterContextFactory factory) { 
        factoryDict[factory.TargetSDType] = factory;
    }

    private bool TryGetExecutor(Type type, out IEncounterExecutor executor) {
        if (executorDict.TryGetValue(type, out executor)) {
            return true;
        }

        executor = null;
        return false;
    }

    private bool TryGetContextFactory(Type type, out IEncounterContextFactory factory) {
        if (factoryDict.TryGetValue(type, out factory)) {
            return true;
        }

        return false;
    }
}
