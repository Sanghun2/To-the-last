using UnityEngine;

public abstract class StrategyBehaviourContainerBase
{
    public abstract void RegisterBehaviour(StrategyBehaviour strategyBehaviour);
    public abstract void RemoveBehaviour(StrategyBehaviour strategyBehaviour);
    public abstract bool TryPullBehaviour(out StrategyBehaviour strategyBehaviour);

    protected abstract bool IsRemainBehaviour();
}
