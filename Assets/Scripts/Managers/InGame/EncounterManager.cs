using System;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : IInitializable
{
    public bool IsInit => _isInit;
    public EncounterContextBase CurrentEncounterContext
    {
        get => currentEncounterContext;
    }

    private Dictionary<string, List<EncounterSDBase>> encounterDict = new();

    private EncounterParserContainer encounterParserContainer = new EncounterParserContainer();
    private EncounterContextBuilderContainer contextBuilderContainer = new EncounterContextBuilderContainer();
    private EncounterExecutorContainer encounterExecutorContainer = new EncounterExecutorContainer();
    private EncounterContextBase currentEncounterContext;
    private bool _isInit;


    public void ExecuteEncounter(EncounterSDBase encounterSD) {
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

        currentEncounterContext = context;
        executor.ExecuteEncounter(context);
    }

    public bool TryGetEncounters(string locationCategoryID, out IReadOnlyList<EncounterSDBase> encounterSDList) {
        if (encounterDict.TryGetValue(locationCategoryID, out var list)) {
            encounterSDList = list;
            return true;
        }

        encounterSDList = null;
        return false;
    }


    #region Management

    public void Init() {
        if (IsInit) return;

        LoadEncounters();

        _isInit = true;
    }
    public void Release() {

    }


    private void LoadEncounters() {
        if (!Managers.SD.TryGetContainer<EncounterSDBase>(out var container)) { return; }

        var encounterSDs = container.SDDict.Values;

        foreach (var encounterSD in encounterSDs) {
            var categoryID = encounterSD.FirstCategory;
            if (string.IsNullOrEmpty(categoryID)) continue;

            if (!encounterDict.TryGetValue(categoryID, out var list)) {
                list = new List<EncounterSDBase>();
            }

            list.Add(encounterSD);
        }
    }

    #endregion
}