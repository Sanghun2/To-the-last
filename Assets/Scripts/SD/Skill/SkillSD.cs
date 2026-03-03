using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSD", menuName = "Scriptable Objects/SkillSD")]
public class SkillSD : IconSDBase
{
    [Space] 
    [SerializeField] StrategyBehaviour.BehaviourType behaviourType;
}
