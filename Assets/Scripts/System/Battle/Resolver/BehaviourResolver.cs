using System;
using System.Collections;
using UnityEngine;

public sealed class BehaviourResolver
{
    private Guid? resolveRoutineID;

    public void ResolveTurnBehaviours(StrategyBehaviourContainerBase containerBase, Action onResolveCompleted=null) {
        if (resolveRoutineID != null) { Debug.LogError($"<color=red>전략 행동이 아직 처리가 되지 않았음.</color>"); return; }

        resolveRoutineID = Managers.Coroutine.StartCoroutine(StrategyBehaviourResolveRoutine(containerBase, onResolveCompleted));
    }

    public void Cancel() {
        if (resolveRoutineID != null) {
            Managers.Coroutine.StopCoroutine(resolveRoutineID.Value);
            resolveRoutineID = null;
        }
    }

    private IEnumerator StrategyBehaviourResolveRoutine(StrategyBehaviourContainerBase strategyBehaviourContainer, Action onResolveCompleted = null) {
        while (strategyBehaviourContainer.TryPullBehaviour(out var strategyBehaviour)) {
            bool isCompleted = false;
            strategyBehaviour.Resolve(() => isCompleted = true);
            while (!isCompleted) {
                yield return null;
            }
        }

        resolveRoutineID = null;
        onResolveCompleted?.Invoke();
    }
}
