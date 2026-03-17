using BilliotGames;
using UnityEngine;

public abstract class ConditionSD : SDBase
{
    public abstract bool IsMet(BattleEntity caster, BattleEntity target);
}
