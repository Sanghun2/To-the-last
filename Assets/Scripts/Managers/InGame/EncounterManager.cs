using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EncounterManager : IInitializable
{
    public bool IsInit => _isInit;
    public EncounterContextBase CurrentEncounterContext
    {
        get => currentEncounterContext;
    }

    private Dictionary<string, List<EncounterSDBase>> encounterDict = new();
    private Dictionary<string, List<EncounterSDBase>> lastEncounterDict = new();

    private EncounterContextBase currentEncounterContext;
    private EncounterDataParserContainer encounterDataParserContainer = new EncounterDataParserContainer();
    private EncounterContextBuilderContainer contextBuilderContainer = new EncounterContextBuilderContainer();
    private EncounterExecutorContainer encounterExecutorContainer = new EncounterExecutorContainer();
    private bool _isInit;


    public void ExecuteEncounter(EncounterSDBase encounterSD) {
        if (encounterDataParserContainer.TryGet(encounterSD, out var parser)) {
            EncounterDataBase encounterData = parser.ParseData(encounterSD);
            ExecuteEncounter(encounterData);
        }
    }
    public void ExecuteEncounter(EncounterDataBase encounterData) {

        Debug.Log($"<color=cyan>[Test] context type: {encounterData.GetType()}</color>");

        if (!contextBuilderContainer.TryGet(encounterData, out var contextBuilder)) {
            Debug.LogError($"<color=red>{encounterData.GetType()}에 해당하는 context factory 없음</color>");
            return;
        }

        var encounterContext = contextBuilder.BuildEncounterContext(encounterData);

        if (!encounterExecutorContainer.TryGet(encounterContext, out var executor)) {
            Debug.LogError($"<color=red>{encounterContext.GetType()}에 해당하는 executor 없음</color>");
            return;
        }

        currentEncounterContext = encounterContext;
        executor.ExecuteEncounter(encounterContext);
    }

    public bool TryGetEncounters(string locationCategoryID, out IReadOnlyList<EncounterSDBase> encounterSDList) {
        if (encounterDict.TryGetValue(locationCategoryID, out var list)) {
            encounterSDList = list;
            return true;
        }

        encounterSDList = null;
        return false;
    }

    public IReadOnlyList<EncounterDataBase> ConvertToEncounterData(IReadOnlyList<EssentialEncounterInfo> essentialLocationEventList) {
        var encounterDataList = new List<EncounterDataBase>();

        for (int i = 0; i < essentialLocationEventList.Count; i++) {
            var essentialEvent = essentialLocationEventList[i];
            encounterDataList.Add(ConvertEncounterData(essentialEvent));
        }

        return encounterDataList;
    }


    public EncounterDataBase GetLastReward(string locationCategoryID) {
        Debug.LogAssertion($"<color=cyan>last reward impletement needed</color>");
        return null;
    }
    public EncounterDataBase GetLastBoss(string locationCategoryID) {
        Debug.LogAssertion($"<color=cyan>last boss impletement needed</color>");
        return null;
    }


    public EncounterDataBase ConvertToEncounterData(EncounterSDBase encounterSD) {
        if (!encounterDataParserContainer.TryGet(encounterSD, out var dataParser)) { Debug.LogError($"<color=red>data parser of ({encounterSD.GetType()}) is not exist</color>"); return null; }
        var data = dataParser.ParseData(encounterSD);
        data.SetIndex(-1);
        return data;
    }
    private EncounterDataBase ConvertEncounterData(EssentialEncounterInfo essentialEvent) {
        if (encounterDataParserContainer.TryGet(essentialEvent.EncounterSD, out var dataParser)) { Debug.LogError($"<color=red>encounter data parser is not exist</color>"); return null; }
        var encounterData = dataParser.ParseData(essentialEvent.EncounterSD);
        encounterData.SetIndex(essentialEvent.Index);

        return encounterData;
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
        encounterDict.Clear();
        lastEncounterDict.Clear();

        if (!Managers.SD.TryGetContainer<EncounterSDBase>(out var container)) { return; }

        var encounterSDs = container.SDDict.Values;

        FilterEncounters(encounterSDs);
    }

    private void FilterEncounters(IEnumerable<EncounterSDBase> encounterSDs) {
        foreach (var encounterSD in encounterSDs) {
            var categoryID = encounterSD.FirstCategory;
            if (string.IsNullOrEmpty(categoryID)) continue;
            if (encounterSD is DialogEncounterSD) continue;

            Dictionary<string, List<EncounterSDBase>> targetDict = null;


            // resolve target dict
            if (encounterSD is ILastEncounterContent) {
                targetDict = lastEncounterDict;
            }
            else {
                targetDict = encounterDict;
            }


            // reigster encounter
            if (!targetDict.TryGetValue(categoryID, out var targetList)) {
                targetList = new List<EncounterSDBase>();
                targetDict.Add(categoryID, targetList);
            }

            targetList.Add(encounterSD);
        }
    }



    #endregion
}