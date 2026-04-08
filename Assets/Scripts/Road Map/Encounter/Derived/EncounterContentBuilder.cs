using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Random = UnityEngine.Random;

public class EncounterContentBuilder : EncounterContentBuilderBase
{
    public override IReadOnlyList<EncounterInfo> BuildContent() {
        int minEncounterCount = 8;
        int maxEncounterCount = 12;
        int encounterCount = Random.Range(minEncounterCount, maxEncounterCount);

        var slots = new List<EncounterInfo>();

        IReadOnlyList<EncounterInfo> _dedicated = GetDedicatedEncounters();
        IReadOnlyList<EncounterInfo> _common = GetCommonEnciybters();
        EncounterInfo _finalBoss = GetLastBoss();
        EncounterInfo _finalReward = GetLastReward();

        Fill(_dedicated, slots);
        Fill(_common, slots);

        slots.Shuffle();

        if (_finalBoss != null) slots.Add(_finalBoss);
        if (_finalReward != null) slots.Add(_finalReward);

        return slots;
    }

    private EncounterInfo GetLastReward() {
        throw new NotImplementedException();
    }

    private EncounterInfo GetLastBoss() {
        throw new NotImplementedException();
    }

    private IReadOnlyList<EncounterInfo> GetCommonEnciybters() {
        throw new NotImplementedException();
    }

    private IReadOnlyList<EncounterInfo> GetDedicatedEncounters() {
        throw new NotImplementedException();
    }

    private void Fill(IReadOnlyList<EncounterInfo> common, List<EncounterInfo> slots) {
        throw new NotImplementedException();
    }
}
