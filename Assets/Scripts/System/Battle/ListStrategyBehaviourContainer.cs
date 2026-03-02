using System;
using System.Collections.Generic;
using UnityEngine;

public class ListStrategyBehaviourContainer : StrategyBehaviourContainerBase
{
    public override int CurrentBehaviourCount => strategyBehaviourList.Count;

    private List<StrategyBehaviour> strategyBehaviourList = new List<StrategyBehaviour>();

    public override void RegisterBehaviour(StrategyBehaviour strategyBehaviour) {
        for (int i = strategyBehaviourList.Count - 1; i >= 0; ++i) {
            if (strategyBehaviourList[i].BehaviourSpeed >= strategyBehaviour.BehaviourSpeed) {
                strategyBehaviourList.Insert(i + 1, strategyBehaviour);
                return;
            }
        }
    }

    public override void RemoveBehaviour(StrategyBehaviour strategyBehaviour) {
        for (int i = 0; i < strategyBehaviourList.Count; i++) {
            if (strategyBehaviourList[i].Equals(strategyBehaviour)) {
                strategyBehaviourList.RemoveAt(i);
                return;
            }
        }
    }

    public override bool TryPullBehaviour(out StrategyBehaviour strategyBehaviour) {
        if (IsRemainBehaviour()) {
            strategyBehaviour = strategyBehaviourList[0];
            strategyBehaviourList.RemoveAt(0);
            return true;
        }

        strategyBehaviour = null;
        return false;
    }

    protected override bool IsRemainBehaviour() {
        return strategyBehaviourList.Count > 0;
    }
}
