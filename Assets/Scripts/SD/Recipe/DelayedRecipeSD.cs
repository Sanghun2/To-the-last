using UnityEngine;

[CreateAssetMenu(fileName = "DelayedRecipeSD", menuName = "Scriptable Objects/Recipe/DelayedRecipeSD")]
public class DelayedRecipeSD : RecipeSD
{
    public int CompletionDelayMinutes => completionDelayMinutes;

    [SerializeField] int completionDelayMinutes;

    private void OnValidate() {
        RenameAsset(ID, suffix:"_DelayedRecipeSD");
    }
}
