using BilliotGames;
using UnityEngine;

public interface IconditionContext { }

public abstract class ConditionSD : SDBase
{
    public abstract bool IsMet(IconditionContext context);
}
