using System;
using System.Collections;
using UnityEngine;

public sealed class BehaviourResolver
{
    private Guid? ResolveRoutineID
    {
        get => _resolveRoutineID;
        set
        {
            _resolveRoutineID = value;
        }
    }

    private Guid? _resolveRoutineID;

    public void ResolveTurnBehaviours(StrategyBehaviourContainerBase containerBase, Action onResolveCompleted=null) {
        if (ResolveRoutineID != null) { Debug.LogError($"<color=red>전략 행동이 아직 처리가 되지 않았음.</color>"); return; }

        ResolveRoutineID = Managers.Coroutine.StartCoroutine(StrategyBehaviourResolveRoutine(containerBase, onResolveCompleted));
    }

    public void Cancel() {
        if (ResolveRoutineID != null) {
            Managers.Coroutine.StopCoroutine((Guid)ResolveRoutineID);
            ResolveRoutineID = null;
        }
    }

    private IEnumerator StrategyBehaviourResolveRoutine(StrategyBehaviourContainerBase strategyBehaviourContainer, Action onResolveCompleted = null) {
        yield return null;
        while (strategyBehaviourContainer.TryPullBehaviour(out var strategyBehaviour)) {
            bool isCompleted = false;
            strategyBehaviour.Resolve(() => isCompleted = true);
            while (!isCompleted) {
                yield return null;
            }
        }

        ResolveRoutineID = null;
        onResolveCompleted?.Invoke();
    }
}
