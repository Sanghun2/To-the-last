using System;
using UnityEngine;

public class ItemUseBehaviour : StrategyBehaviour
{
    public ItemUseBehaviour(BattleEntity owner, BehaviourType behaviourType, int behaviourSpeed) : base(owner, behaviourType, behaviourSpeed) {
    }

    public override void Resolve(Action onResolveCompleted = null) {
        throw new NotImplementedException();
    }
}
