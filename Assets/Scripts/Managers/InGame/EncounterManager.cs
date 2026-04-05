using System;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : IInitializable
{
    public bool IsInit => _isInit;

    private EncounterParserContainer encounterParserContainer = new EncounterParserContainer();
    private EncounterContextBuilderContainer contextBuilderContainer = new EncounterContextBuilderContainer();
    private EncounterExecutorContainer encounterExecutorContainer = new EncounterExecutorContainer(); 
    private bool _isInit;


    public void ExecuteEncounter(EncounterSD encounterSD) {
        if (encounterParserContainer.TryGet(encounterSD, out var parser)) {
            if (!parser.TryParse(encounterSD, out EncounterDataBase data)) { Debug.LogError($"<color=red>({encounterSD.GetType()}) parser not exist</color>"); return; }

            ExecuteEncounter(data);
        }
    }
    public void ExecuteEncounter(EncounterDataBase encounterData) {

        Debug.Log($"<color=cyan>[Test] context type: {encounterData.GetType()}</color>");

        if (!contextBuilderContainer.TryGet(encounterData, out var contextBuilder)) {
            Debug.LogError($"<color=red>{encounterData.GetType()}에 해당하는 context factory 없음</color>");
            return;
        }

        var context = contextBuilder.BuildContext(encounterData);
        if (context == null) {
            Debug.LogError($"<color=red>생성된 context null</color>");
            return;
        }

        //var contextType = context.GetType();
        Debug.Log($"context type: {context.GetType()}");
        if (!encounterExecutorContainer.TryGet(context, out var executor)) {
            Debug.LogError($"<color=red>{context.GetType()}에 해당하는 executor 없음</color>");
        }

        executor.ExecuteEncounter(context);
    }


    #region Management

    public void Init() {
        if (IsInit) return;

        // parser
        encounterParserContainer.Register<LootEncounterSD>(new LootEncounterParser());

        // context builder
        contextBuilderContainer.Register<LootEncounterData>(new LootEncounterContextBuilder());

        // executor
        encounterExecutorContainer.Register<LootEncounterContext>(new LootEncounterExecutor());

        _isInit = true;
    }
    public void Release() {

    }

    #endregion
}