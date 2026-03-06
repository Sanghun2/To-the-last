using System;
using UnityEngine;

public class SkillBehaviour : StrategyBehaviour
{
    public SkillBehaviour(BattleEntity caster, BehaviourType behaviourType, int behaviourSpeed) : base(caster, behaviourType, behaviourSpeed) {
    }

    internal override void Resolve(Action onResolveCompleted = null) {
        throw new NotImplementedException();
    }
}
