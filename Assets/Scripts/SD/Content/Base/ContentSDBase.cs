using System.Collections.Generic;
using UnityEngine;

public abstract class ContentSDBase : TimeSDBase
{
    public IReadOnlyList<Ingredient> Requirements => requirements;
    public int RequiredLevel => requiredLevel;
    public string ExecutionButtonText => executionButtonText;

    [SerializeField] protected int requiredLevel;
    [SerializeField] protected Ingredient[] requirements;
    [SerializeField] protected string executionButtonText;

    public void SetDefaultExecutionButtonText(string buttonText) {
        executionButtonText = buttonText;
    }
}
