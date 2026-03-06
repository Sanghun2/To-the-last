using System;
using UnityEngine;

public class SkillBehaviour : StrategyBehaviour
{
    public SkillBehaviour(
        BattleEntity caster, 
        BehaviourType behaviourType, 
        int behaviourSpeed,
        BattleEntity target=null) : base(caster, behaviourType, behaviourSpeed, target) {
    }

    internal override void Resolve(Action onResolveCompleted = null) {
        throw new NotImplementedException();
    }
}
