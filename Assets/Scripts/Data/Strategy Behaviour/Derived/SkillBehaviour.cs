using System;
using UnityEngine;

public class SkillBehaviour : StrategyBehaviour
{
    [SerializeField] SkillSD skillSD;

    public SkillBehaviour(
        BattleEntity caster, 
        SkillSD skillSD,
        BattleEntity target=null) : base(
            caster, 
            skillSD.BehaviourType,
            (int)BattleUtility.CalculateBehaviourSpeed(caster), 
            target) 
        {
        this.skillSD = skillSD;
    }

    public override void Resolve(Action onResolveCompleted = null) {
        throw new NotImplementedException();
    }
}
