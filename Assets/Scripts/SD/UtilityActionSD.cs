using UnityEngine;

[CreateAssetMenu(fileName = "ActionSD", menuName = "Scriptable Objects/UtilityActionSD")]
public class UtilityActionSD : TimeBasedSD
{
    [SerializeField] Ingredient[] inputs;
    //[SerializeField] effect

    public void ApplyEffect() {

    }
}
