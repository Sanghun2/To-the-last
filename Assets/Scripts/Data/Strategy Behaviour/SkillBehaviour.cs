using System;
using UnityEngine;

public class SkillBehaviour : StrategyBehaviour
{
    public SkillBehaviour(BattleEntity owner, BehaviourType behaviourType, int behaviourSpeed) : base(owner, behaviourType, behaviourSpeed) {
    }

    internal override void Resolve(Action onResolveCompleted = null) {
        throw new NotImplementedException();
    }
}
