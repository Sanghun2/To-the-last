using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public sealed class EncounterMapBuilder : EncounterMapBuilderBase<EncounterMapContext>
{

    public override IReadOnlyList<EncounterDataBase> BuildMap(EncounterMapContext mapContext) {
        int minEncounterCount = mapContext.MinEncounterCount;
        int maxEncounterCount = mapContext.MaxEncounterCount;
        int encounterCount = Random.Range(minEncounterCount, maxEncounterCount+1);

        var encounterList = new List<EncounterDataBase>(encounterCount);

        // 전용 encounter, 공통 encounter 추가
        string locationCategoryID = mapContext.LocationCategoryID;
        int minDedicatedEncounterCount = 2;
        int maxDedicatedEncounterCount = 5;
        IReadOnlyList<EncounterDataBase> _dedicated = GetDedicatedEncounters(locationCategoryID);
        IReadOnlyList<EncounterDataBase> _common = GetCommonEncounters();
        
        Fill(_dedicated, encounterList, minDedicatedEncounterCount, maxDedicatedEncounterCount);
        FillRest(_common, encounterList, encounterCount);

        // 전용, 공통 encounter 순서 shuffle
        Shuffle(encounterList);

        // 필수 encounter 추가
        InsertList(encounterList, mapContext.EssentialEncounterList);

        // 최종 보스, 리워드 추가
        EncounterDataBase _finalBoss = GetLastBoss(locationCategoryID);
        EncounterDataBase _finalReward = GetLastReward(locationCategoryID);

        if (_finalBoss != null) encounterList.Add(_finalBoss);
        if (_finalReward != null) encounterList.Add(_finalReward);

        return encounterList;
    }

    private void InsertList(List<EncounterDataBase> baseEncounterList, IReadOnlyList<EncounterDataBase> essentialEncounterList) {
        foreach (var essential in essentialEncounterList) {
            int targetIndex = essential.Index;

            // -1 같은 값 처리 (정책 선택)
            if (targetIndex < 0) {
                baseEncounterList.Add(essential);
                continue;
            }

            // 범위 보정
            if (targetIndex > baseEncounterList.Count) {
                targetIndex = baseEncounterList.Count;
            }

            baseEncounterList.Insert(targetIndex, essential);
        }
    }

    private void Shuffle(List<EncounterDataBase> encounterList) {
        // 1. index == -1 인 요소들의 위치와 값 수집
        List<int> targetIndices = new List<int>();
        List<EncounterDataBase> targets = new List<EncounterDataBase>();

        for (int i = 0; i < encounterList.Count; i++) {
            if (encounterList[i].Index == -1) {
                targetIndices.Add(i);
                targets.Add(encounterList[i]);
            }
        }

        // 2. 대상만 셔플
        for (int i = targets.Count - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            (targets[i], targets[j]) = (targets[j], targets[i]);
        }

        // 3. 원래 위치에 다시 넣기
        for (int i = 0; i < targetIndices.Count; i++) {
            encounterList[targetIndices[i]] = targets[i];
        }
    }

    private EncounterDataBase GetLastReward(string locationCategoryID) {
        return Managers.Encounter.GetLastReward(locationCategoryID);
    }

    private EncounterDataBase GetLastBoss(string locationCategoryID) {
        return Managers.Encounter.GetLastBoss(locationCategoryID);
    }

    private IReadOnlyList<EncounterDataBase> GetCommonEncounters() {
        if (Managers.Encounter.TryGetEncounters("common", out var encounterSDList)) {
            var encounterManager = Managers.Encounter;
            return encounterSDList.Select(x => encounterManager.ConvertToEncounterData(x)).ToList();
        }

        return null;
    }

    private IReadOnlyList<EncounterDataBase> GetDedicatedEncounters(string locationCategoryID) {
        if (Managers.Encounter.TryGetEncounters(locationCategoryID, out var encounterSDList)) {
            var encounterManager = Managers.Encounter;
            return encounterSDList.Select(x => encounterManager.ConvertToEncounterData(x)).ToList();
        }

        return null;
    }

    private void Fill(IReadOnlyList<EncounterDataBase> encounterList, List<EncounterDataBase> baseEncounterList, int minEncounterCount, int maxEncounterCount) {
        if (encounterList == null || encounterList.Count == 0) return;

        int count = Random.Range(minEncounterCount, maxEncounterCount+1);
        ChooseEncounters(encounterList, baseEncounterList, count);
    }
    private void FillRest(IReadOnlyList<EncounterDataBase> encounterList, List<EncounterDataBase> baseEncounterList, int limitCount) {
        if (encounterList == null || encounterList.Count == 0) return;
        int remainCount = limitCount - baseEncounterList.Count;
        ChooseEncounters(encounterList, baseEncounterList, remainCount);
    }

    private void ChooseEncounters(IReadOnlyList<EncounterDataBase> encounterList, List<EncounterDataBase> baseEncounterList, int count) {
        if (encounterList == null || encounterList.Count == 0) return; 
        for (int i = 0; i < count; i++) {
            var targetEncounter = encounterList[Random.Range(0, encounterList.Count)];
            baseEncounterList.Add(targetEncounter);
        }
    }
}
