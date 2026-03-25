using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public sealed class MetabolicSystem
{
    private List<(Define.Stat stat, float value)> consumeList = new List<(Define.Stat, float)>();
    //private bool isInit;

    public void InitMetabolism(IReadOnlyList<(Define.Stat stat, float consumeAmount)> newConsumeList) {
        consumeList.Clear();
        for (int i = 0; i < newConsumeList.Count; i++) {
            consumeList.Add(newConsumeList[i]);
        }
    }

    public void ConsumeStats(StatContainer statContainer, int multiplier) {
        for (int i = 0; i < consumeList.Count; i++) {
            var data = consumeList[i];
            var targetStat = data.stat;
            var value = data.value;
            var applyValue = value * multiplier;

            if (statContainer.TryGetStat(targetStat.ToID(), out Stat stat)) {
                stat.ChangeRawValue(-applyValue);
                //Debug.Log($"[Test] ({targetStat}) ({-applyValue})");
            }
        }
    }
}
