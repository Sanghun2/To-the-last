using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSD", menuName = "Scriptable Objects/SkillSD")]
public class SkillSD : IconSDBase
{
    public IReadOnlyList<EffectSD> Effects => effects;
    public StrategyBehaviour.BehaviourType BehaviourType => behaviourType;

    [Space] 
    [SerializeField] StrategyBehaviour.BehaviourType behaviourType = StrategyBehaviour.BehaviourType.Normal;

    [Space]
    [SerializeField] EffectSD[] effects;

    private void OnValidate() {
        RenameAsset(ID, suffix:"_SkillSD");
    }
}
