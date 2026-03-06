using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionSD", menuName = "Scriptable Objects/ConditionSD")]
public abstract class ConditionSD : SDBase
{
    public abstract bool IsMet(BattleEntity caster, BattleEntity target);
}
