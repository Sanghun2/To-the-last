using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Random = UnityEngine.Random;

public class EncounterContentBuilder : EncounterContentBuilderBase
{
    public override IReadOnlyList<EncounterSelectCondition> BuildContent() {
        int minEncounterCount = 8;
        int maxEncounterCount = 12;
        int encounterCount = Random.Range(minEncounterCount, maxEncounterCount);

        var slots = new List<EncounterSelectCondition>();

        IReadOnlyList<EncounterSelectCondition> _dedicated = GetDedicatedEncounters();
        IReadOnlyList<EncounterSelectCondition> _common = GetCommonEnciybters();
        EncounterSelectCondition _finalBoss = GetLastBoss();
        EncounterSelectCondition _finalReward = GetLastReward();

        Fill(_dedicated, slots);
        Fill(_common, slots);

        slots.Shuffle();

        if (_finalBoss != null) slots.Add(_finalBoss);
        if (_finalReward != null) slots.Add(_finalReward);

        return slots;
    }

    private EncounterSelectCondition GetLastReward() {
        throw new NotImplementedException();
    }

    private EncounterSelectCondition GetLastBoss() {
        throw new NotImplementedException();
    }

    private IReadOnlyList<EncounterSelectCondition> GetCommonEnciybters() {
        throw new NotImplementedException();
    }

    private IReadOnlyList<EncounterSelectCondition> GetDedicatedEncounters() {
        throw new NotImplementedException();
    }

    private void Fill(IReadOnlyList<EncounterSelectCondition> common, List<EncounterSelectCondition> slots) {
        throw new NotImplementedException();
    }
}
